﻿using Moq;
using System.ComponentModel;
using ThePensionsRegulator.Umbraco.Blocks;
using ThePensionsRegulator.Umbraco.PropertyEditors;
using ThePensionsRegulator.Umbraco.Testing;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace ThePensionsRegulator.Umbraco.Tests.Blocks
{
    public class OverridableBlockGridModelTests
    {
        private const string PROPERTY_ALIAS_CHILD_BLOCKS = "childBlocks";

        private static (OverridableBlockGridModel ParentBlockGrid, OverridableBlockGridModel ChildBlockGrid, OverridableBlockGridModel GrandChildBlockGrid) CreateThreeNestedOverridableBlockGrids()
        {
            var grandChildBlockGrid = UmbracoBlockGridFactory.CreateOverridableBlockGridModel(Array.Empty<BlockGridItem>());

            var childContent = UmbracoBlockGridFactory.CreateContentOrSettings()
                    .SetupUmbracoBlockGridPropertyValue(PROPERTY_ALIAS_CHILD_BLOCKS, grandChildBlockGrid)
                    .Object;

            var childBlockGrid = UmbracoBlockGridFactory.CreateOverridableBlockGridModel(
                UmbracoBlockGridFactory.CreateOverridableBlock(childContent)
                );

            var parentContent = UmbracoBlockGridFactory.CreateContentOrSettings()
                    .SetupUmbracoBlockGridPropertyValue(PROPERTY_ALIAS_CHILD_BLOCKS, childBlockGrid)
                    .Object;

            var parentBlockGrid = UmbracoBlockGridFactory.CreateOverridableBlockGridModel(
                UmbracoBlockGridFactory.CreateOverridableBlock(parentContent)
                );

            return (parentBlockGrid, childBlockGrid, grandChildBlockGrid);
        }

        private static (OverridableBlockGridModel ParentBlockGrid, OverridableBlockListModel ChildBlockList, OverridableBlockListModel GrandChildBlockList) CreateOverridableBlockGridModelWithTwoNestedOverridableBlockLists()
        {
            var parentBlock = CreateOverridableBlockGridItemWithTwoNestedOverridableBlockLists();

            var parentBlockGrid = UmbracoBlockGridFactory.CreateOverridableBlockGridModel(parentBlock.ParentBlock);

            return (parentBlockGrid, parentBlock.ChildBlockList, parentBlock.GrandChildBlockList);
        }

        private static (OverridableBlockGridItem ParentBlock, OverridableBlockListModel ChildBlockList, OverridableBlockListModel GrandChildBlockList) CreateOverridableBlockGridItemWithTwoNestedOverridableBlockLists()
        {
            var grandChildBlockList = UmbracoBlockListFactory.CreateOverridableBlockListModel(Array.Empty<BlockListItem>());

            var childContent = UmbracoBlockListFactory.CreateContentOrSettings()
                    .SetupUmbracoBlockListPropertyValue(PROPERTY_ALIAS_CHILD_BLOCKS, grandChildBlockList)
                    .Object;

            var childBlockList = UmbracoBlockListFactory.CreateOverridableBlockListModel(
                UmbracoBlockListFactory.CreateOverridableBlock(childContent)
                );

            var parentContent = UmbracoBlockGridFactory.CreateContentOrSettings()
                    .SetupUmbracoBlockListPropertyValue(PROPERTY_ALIAS_CHILD_BLOCKS, childBlockList)
                    .Object;

            var parentBlock = UmbracoBlockGridFactory.CreateOverridableBlock(parentContent);

            return (parentBlock, childBlockList, grandChildBlockList);
        }

        [Test]
        public void BlockGridModels_are_converted_to_OverridableBlockGridModels_including_nested_block_grids()
        {
            // Arrange
            var grandChildBlockGrid = UmbracoBlockGridFactory.CreateBlockGridModel(Array.Empty<BlockGridItem>());
            var childBlockGrid = UmbracoBlockGridFactory.CreateBlockGridModel(
                UmbracoBlockGridFactory.CreateBlock(
                    UmbracoBlockGridFactory.CreateContentOrSettings()
                    .SetupUmbracoBlockGridPropertyValue(PROPERTY_ALIAS_CHILD_BLOCKS, grandChildBlockGrid)
                    .Object
                    )
                );
            var parentBlockGrid = UmbracoBlockGridFactory.CreateBlockGridModel(
                UmbracoBlockGridFactory.CreateBlock(
                    UmbracoBlockGridFactory.CreateContentOrSettings()
                    .SetupUmbracoBlockGridPropertyValue(PROPERTY_ALIAS_CHILD_BLOCKS, childBlockGrid)
                    .Object
                    )
                );

            OverridableBlockGridModel? convertedChildBlockGrid = null, convertedGrandChildBlockGrid = null;

            var parentBlockGridContent = new Mock<IOverridablePublishedElement>();
            parentBlockGridContent.Setup(x => x.OverrideValue(PROPERTY_ALIAS_CHILD_BLOCKS, It.IsAny<object>())).Callback<string, object>((alias, value) =>
            {
                convertedChildBlockGrid = value as OverridableBlockGridModel;
            });
            parentBlockGridContent.Setup(x => x.Properties).Returns(parentBlockGrid[0].Content.Properties);
            parentBlockGridContent.Setup(x => x.Value<BlockGridModel>(PROPERTY_ALIAS_CHILD_BLOCKS, null, null, default, default)).Returns(childBlockGrid);

            var childBlockGridContent = new Mock<IOverridablePublishedElement>();
            childBlockGridContent.Setup(x => x.OverrideValue(PROPERTY_ALIAS_CHILD_BLOCKS, It.IsAny<object>())).Callback<string, object>((alias, value) =>
            {
                convertedGrandChildBlockGrid = value as OverridableBlockGridModel;
            });
            childBlockGridContent.Setup(x => x.Properties).Returns(childBlockGrid[0].Content.Properties);
            childBlockGridContent.Setup(x => x.Value<BlockGridModel>(PROPERTY_ALIAS_CHILD_BLOCKS, null, null, default, default)).Returns(grandChildBlockGrid);

            var factoryCalls = 0;
            Func<IPublishedElement?, IOverridablePublishedElement?> factory = x =>
            {
                factoryCalls++;
                switch (factoryCalls)

                {
                    case 1:
                        return parentBlockGridContent.Object;
                    case 3:
                        return childBlockGridContent.Object;
                    default:
                        return null;
                }
            };

            // Act
            _ = new OverridableBlockGridModel(parentBlockGrid, null, factory);

            // Assert
            Assert.NotNull(convertedChildBlockGrid);
            Assert.NotNull(convertedGrandChildBlockGrid);
        }

        [Test]
        public void BlockListModels_are_converted_to_OverridableBlockListModels_including_nested_block_lists()
        {
            // Arrange
            var grandChildBlockList = UmbracoBlockListFactory.CreateBlockListModel(Array.Empty<BlockListItem>());
            var childBlockList = UmbracoBlockListFactory.CreateBlockListModel(
                UmbracoBlockListFactory.CreateBlock(
                    UmbracoBlockListFactory.CreateContentOrSettings()
                    .SetupUmbracoBlockListPropertyValue(PROPERTY_ALIAS_CHILD_BLOCKS, grandChildBlockList)
                    .Object
                    )
                );
            var parentBlockGrid = UmbracoBlockGridFactory.CreateBlockGridModel(
                UmbracoBlockGridFactory.CreateBlock(
                    UmbracoBlockGridFactory.CreateContentOrSettings()
                    .SetupUmbracoBlockListPropertyValue(PROPERTY_ALIAS_CHILD_BLOCKS, childBlockList)
                    .Object
                    )
                );

            OverridableBlockListModel? convertedChildBlockList = null, convertedGrandChildBlockList = null;

            var parentBlockGridContent = new Mock<IOverridablePublishedElement>();
            parentBlockGridContent.Setup(x => x.OverrideValue(PROPERTY_ALIAS_CHILD_BLOCKS, It.IsAny<object>())).Callback<string, object>((alias, value) =>
            {
                convertedChildBlockList = value as OverridableBlockListModel;
            });
            parentBlockGridContent.Setup(x => x.Properties).Returns(parentBlockGrid[0].Content.Properties);
            parentBlockGridContent.Setup(x => x.Value<BlockListModel>(PROPERTY_ALIAS_CHILD_BLOCKS, null, null, default, default)).Returns(childBlockList);

            var childBlockListContent = new Mock<IOverridablePublishedElement>();
            childBlockListContent.Setup(x => x.OverrideValue(PROPERTY_ALIAS_CHILD_BLOCKS, It.IsAny<object>())).Callback<string, object>((alias, value) =>
            {
                convertedGrandChildBlockList = value as OverridableBlockListModel;
            });
            childBlockListContent.Setup(x => x.Properties).Returns(childBlockList[0].Content.Properties);
            childBlockListContent.Setup(x => x.Value<BlockListModel>(PROPERTY_ALIAS_CHILD_BLOCKS, null, null, default, default)).Returns(grandChildBlockList);

            var factoryCalls = 0;
            Func<IPublishedElement?, IOverridablePublishedElement?> factory = x =>
            {
                factoryCalls++;
                switch (factoryCalls)

                {
                    case 1:
                        return parentBlockGridContent.Object;
                    case 3:
                        return childBlockListContent.Object;
                    default:
                        return null;
                }
            };

            // Act
            _ = new OverridableBlockGridModel(parentBlockGrid, null, factory);

            // Assert
            Assert.NotNull(convertedChildBlockList);
            Assert.NotNull(convertedGrandChildBlockList);
        }

        [Test]
        public void BlockGridAreas_are_converted_to_OverridableBlockGridAreas()
        {
            // Arrange
            var itemInArea = UmbracoBlockGridFactory.CreateBlock(
                                UmbracoBlockGridFactory.CreateContentOrSettings("inArea").Object
                             );

            var blockGrid = UmbracoBlockGridFactory.CreateBlockGridModel(
                               UmbracoBlockGridFactory.CreateBlock(
                                   UmbracoBlockGridFactory.CreateContentOrSettings("alias").Object
                                   )
                               );

            const string AREA_ALIAS = "area51";
            const int AREA_ROWSPAN = 2;
            const int AREA_COLSPAN = 4;
            blockGrid[0].Areas = new List<BlockGridArea>
            {
                new BlockGridArea(new List<BlockGridItem>{ itemInArea }, AREA_ALIAS, AREA_ROWSPAN, AREA_COLSPAN)
            };

            // Act
            var result = new OverridableBlockGridModel(blockGrid);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Areas.Count(), Is.EqualTo(1));
                Assert.That(result[0].Areas.First().Count(), Is.EqualTo(1));
                Assert.That(result[0].Areas.First().Alias, Is.EqualTo(AREA_ALIAS));
                Assert.That(result[0].Areas.First().RowSpan, Is.EqualTo(AREA_ROWSPAN));
                Assert.That(result[0].Areas.First().ColumnSpan, Is.EqualTo(AREA_COLSPAN));
                Assert.That(result[0].Areas.First().First().Content.ContentType.Alias, Is.EqualTo("inArea"));
            });
        }

        [Test]
        public void Filter_is_passed_down_from_constructor_to_child_models_and_areas()
        {
            // Arrange
            var blockGrids = CreateOverridableBlockGridModelWithTwoNestedOverridableBlockLists();
            blockGrids.ParentBlockGrid[0].Areas = new List<OverridableBlockGridArea>
            {
                new OverridableBlockGridArea(new []{ CreateOverridableBlockGridItemWithTwoNestedOverridableBlockLists().ParentBlock }, "area",1,1)
            };

            var filter = new Func<IOverridableBlockReference<IOverridablePublishedElement, IOverridablePublishedElement>, bool>(block => true);

            // Act
            var model = new OverridableBlockGridModel(blockGrids.ParentBlockGrid, filter);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(model.Filter, Is.EqualTo(filter));

                var childBlockList = model[0].Content.Value<OverridableBlockListModel>(PROPERTY_ALIAS_CHILD_BLOCKS);
                Assert.That(childBlockList!.Filter, Is.EqualTo(filter));

                var grandchildBlockList = childBlockList[0].Content.Value<OverridableBlockListModel>(PROPERTY_ALIAS_CHILD_BLOCKS);
                Assert.That(grandchildBlockList!.Filter, Is.EqualTo(filter));

                Assert.That(model[0].Areas[0].Filter, Is.EqualTo(filter));

                var areaChildBlockList = model[0].Areas[0][0].Content.Value<OverridableBlockListModel>(PROPERTY_ALIAS_CHILD_BLOCKS);
                Assert.That(areaChildBlockList!.Filter, Is.EqualTo(filter));

                var areaGrandchildBlockList = areaChildBlockList[0].Content.Value<OverridableBlockListModel>(PROPERTY_ALIAS_CHILD_BLOCKS);
                Assert.That(areaGrandchildBlockList!.Filter, Is.EqualTo(filter));
            });
        }

        [Test]
        public void Filter_is_passed_down_from_setter_to_child_models_and_areas()
        {
            // Arrange
            var blockGrids = CreateOverridableBlockGridModelWithTwoNestedOverridableBlockLists();
            blockGrids.ParentBlockGrid[0].Areas = new List<OverridableBlockGridArea>
            {
                new OverridableBlockGridArea(new []{ CreateOverridableBlockGridItemWithTwoNestedOverridableBlockLists().ParentBlock }, "area",1,1)
            };

            var filter = new Func<IOverridableBlockReference<IOverridablePublishedElement, IOverridablePublishedElement>, bool>(block => true);

            // Act
            var model = blockGrids.ParentBlockGrid;
            model.Filter = filter;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(model.Filter, Is.EqualTo(filter));

                var childBlockList = model[0].Content.Value<OverridableBlockListModel>(PROPERTY_ALIAS_CHILD_BLOCKS);
                Assert.That(childBlockList!.Filter, Is.EqualTo(filter));

                Assert.That(model[0].Areas[0].Filter, Is.EqualTo(filter));

                var areaChildBlockList = model[0].Areas[0][0].Content.Value<OverridableBlockListModel>(PROPERTY_ALIAS_CHILD_BLOCKS);
                Assert.That(areaChildBlockList!.Filter, Is.EqualTo(filter));

                var areaGrandchildBlockList = areaChildBlockList[0].Content.Value<OverridableBlockListModel>(PROPERTY_ALIAS_CHILD_BLOCKS);
                Assert.That(areaGrandchildBlockList!.Filter, Is.EqualTo(filter));
            });
        }

        [Test]
        public void Indexer_acts_on_unfiltered_blocks()
        {
            // Arrange
            var blockGrid = UmbracoBlockGridFactory.CreateOverridableBlockGridModel(
                                UmbracoBlockGridFactory.CreateOverridableBlock(
                                    UmbracoBlockGridFactory.CreateContentOrSettings("alias").Object
                                    )
                                );

            blockGrid.Filter = block => false;

            // Act + Assert
            Assert.That(() => blockGrid[0], Throws.Nothing);
        }

        [Test]
        public void PropertyValueFormatters_are_passed_down_to_direct_child_items()
        {
            // Arrange
            var formatter = Mock.Of<IPropertyValueFormatter>();
            var blockGrid = UmbracoBlockGridFactory.CreateOverridableBlockGridModel(
                UmbracoBlockGridFactory.CreateOverridableBlock(
                    new OverridablePublishedElement(UmbracoBlockGridFactory.CreateContentOrSettings().Object),
                    new OverridablePublishedElement(UmbracoBlockGridFactory.CreateContentOrSettings().Object)
                ));

            // Act
            blockGrid.PropertyValueFormatters = new List<IPropertyValueFormatter> { formatter };

            // Assert
            Assert.That(((OverridablePublishedElement)blockGrid[0].Content).PropertyValueFormatters?.Count(), Is.EqualTo(1));
            Assert.That(((OverridablePublishedElement)blockGrid[0].Settings).PropertyValueFormatters?.Count(), Is.EqualTo(1));
        }

        [Test]
        public void PropertyValueFormatters_are_passed_down_to_child_items_of_areas()
        {
            // Arrange
            var formatter = Mock.Of<IPropertyValueFormatter>();
            var block = UmbracoBlockGridFactory.CreateOverridableBlock(
                    new OverridablePublishedElement(UmbracoBlockGridFactory.CreateContentOrSettings().Object),
                    new OverridablePublishedElement(UmbracoBlockGridFactory.CreateContentOrSettings().Object)
                );
            block.Areas = new List<OverridableBlockGridArea> {
                new OverridableBlockGridArea(new []
                {
                    UmbracoBlockGridFactory.CreateOverridableBlock(
                        new OverridablePublishedElement(UmbracoBlockGridFactory.CreateContentOrSettings().Object),
                        new OverridablePublishedElement(UmbracoBlockGridFactory.CreateContentOrSettings().Object)
                    )
                }, "area", 1,1)
            };

            var blockGrid = UmbracoBlockGridFactory.CreateOverridableBlockGridModel(block);

            // Act
            blockGrid.PropertyValueFormatters = new List<IPropertyValueFormatter> { formatter };

            // Assert
            var blockWithinArea = blockGrid[0].Areas.First()[0];
            Assert.That(((OverridablePublishedElement)blockWithinArea.Content).PropertyValueFormatters?.Count(), Is.EqualTo(1));
            Assert.That(((OverridablePublishedElement)blockWithinArea.Settings).PropertyValueFormatters?.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Can_cast_to_BlockGridModel()
        {
            // Arrange
            var blockGrid = UmbracoBlockGridFactory.CreateOverridableBlockGridModel(
                                UmbracoBlockGridFactory.CreateOverridableBlock(
                                    UmbracoBlockGridFactory.CreateContentOrSettings("alias").Object
                                    )
                                );

            // Act
            var model = (BlockGridModel)blockGrid;

            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(blockGrid.Count()));
        }

        [Test]
        public void Can_convert_to_BlockGridModel()
        {
            var converter = TypeDescriptor.GetConverter(new OverridableBlockGridModel());

            Assert.That(converter.GetType(), Is.EqualTo(typeof(OverridableBlockGridTypeConverter)));
        }

        [Test]
        public void Can_cast_to_IEnumerable_of_OverridableBlockGridItem()
        {
            // Arrange
            var blockGrid = UmbracoBlockGridFactory.CreateOverridableBlockGridModel(
                                UmbracoBlockGridFactory.CreateOverridableBlock(
                                    UmbracoBlockGridFactory.CreateContentOrSettings("alias").Object
                                    )
                                );

            // Act
            var model = (IEnumerable<OverridableBlockGridItem>)blockGrid;

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(blockGrid.Count()));
        }

        [Test]
        public void Can_cast_to_IEnumerable_of_BlockGridItem()
        {
            // Arrange
            var blockGrid = UmbracoBlockGridFactory.CreateOverridableBlockGridModel(
                                UmbracoBlockGridFactory.CreateOverridableBlock(
                                    UmbracoBlockGridFactory.CreateContentOrSettings("alias").Object
                                    )
                                );

            // Act
            var model = (IEnumerable<BlockGridItem>)blockGrid;

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(blockGrid.Count()));
        }


        [Test]
        public void Can_cast_to_IEnumerable_of_IOverridableBlockReference()
        {
            // Arrange
            var blockGrid = UmbracoBlockGridFactory.CreateOverridableBlockGridModel(
                                UmbracoBlockGridFactory.CreateOverridableBlock(
                                    UmbracoBlockGridFactory.CreateContentOrSettings("alias").Object
                                    )
                                );

            // Act
            var model = (IEnumerable<IOverridableBlockReference<IOverridablePublishedElement, IOverridablePublishedElement>>)blockGrid;

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(blockGrid.Count()));
        }


        [Test]
        public void Can_cast_to_IEnumerable_of_IBlockReference()
        {
            // Arrange
            var blockGrid = UmbracoBlockGridFactory.CreateOverridableBlockGridModel(
                                UmbracoBlockGridFactory.CreateOverridableBlock(
                                    UmbracoBlockGridFactory.CreateContentOrSettings("alias").Object
                                    )
                                );

            // Act
            var model = (IEnumerable<IBlockReference<IPublishedElement, IPublishedElement>>)blockGrid;

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(blockGrid.Count()));
        }
    }
}
