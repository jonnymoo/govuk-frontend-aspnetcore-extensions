﻿using GovUk.Frontend.Umbraco.Models;
using GovUk.Frontend.Umbraco.Testing;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace GovUk.Frontend.Umbraco.Tests
{
    public class BlockListModelExtensionsTests
    {
        [Test]
        public void Block_is_matched_in_root_block_list()
        {
            var blockList = UmbracoBlockListFactory.CreateBlockListModel(
                UmbracoBlockListFactory.CreateBlock(
                    UmbracoBlockListFactory.CreateContentOrSettings()
                    .SetupUmbracoTextboxPropertyValue("MyTextProperty", "value")
                    .Object
                )
            );

            // Act
            var result = BlockListModelExtensions.FindBlock(blockList, x => x.Content.GetProperty("MyTextProperty") != null);

            // Assert
            Assert.AreEqual(blockList.First(), result);
        }

        [Test]
        public void Block_is_matched_in_descendant_block_list()
        {
            var grandChildBlockList = UmbracoBlockListFactory.CreateBlockListModel(
                UmbracoBlockListFactory.CreateBlock(
                    UmbracoBlockListFactory.CreateContentOrSettings()
                    .SetupUmbracoTextboxPropertyValue("MyTextProperty", "value")
                    .Object
                )
            );

            var childBlockList = UmbracoBlockListFactory.CreateBlockListModel(
                UmbracoBlockListFactory.CreateBlock(
                    UmbracoBlockListFactory.CreateContentOrSettings()
                    .SetupUmbracoBlockListPropertyValue("grandchildBlocks", grandChildBlockList)
                    .Object
                    )
                );

            var parentBlockList = UmbracoBlockListFactory.CreateBlockListModel(
                UmbracoBlockListFactory.CreateBlock(
                    UmbracoBlockListFactory.CreateContentOrSettings()
                    .SetupUmbracoBlockListPropertyValue("childBlocks", childBlockList)
                    .Object
                )
            );

            // Act
            var result = BlockListModelExtensions.FindBlock(parentBlockList, x => x.Content.GetProperty("MyTextProperty") != null);

            // Assert
            Assert.AreEqual(grandChildBlockList.First(), result);
        }


        [Test]
        public void Multiple_matching_blocks_are_matched_in_descendant_block_list()
        {
            var matchingBlockContent1 = new Mock<IOverridablePublishedElement>();
            matchingBlockContent1.Setup(x => x.GetProperty("MyTextProperty")).Returns(UmbracoPropertyFactory.CreateTextboxProperty("MyTextProperty", "value"));

            var matchingBlock1 = new OverridableBlockListItem(
                new BlockListItem(Udi.Create(Constants.UdiEntityType.Element, Guid.NewGuid()), matchingBlockContent1.Object, null, null),
                x => (IOverridablePublishedElement)x
            );

            var matchingBlockContent2 = new Mock<IOverridablePublishedElement>();
            matchingBlockContent1.Setup(x => x.GetProperty("MyTextProperty")).Returns(UmbracoPropertyFactory.CreateTextboxProperty("MyTextProperty", "value"));

            var matchingBlock2 = new OverridableBlockListItem(
                new BlockListItem(Udi.Create(Constants.UdiEntityType.Element, Guid.NewGuid()), matchingBlockContent1.Object, null, null),
                x => (IOverridablePublishedElement)x
            );

            var grandChildBlockList = new BlockListModel(new[] { matchingBlock1, matchingBlock2 });

            var childBlockList = UmbracoBlockListFactory.CreateBlockListModel(
                UmbracoBlockListFactory.CreateBlock(
                    UmbracoBlockListFactory.CreateContentOrSettings()
                    .SetupUmbracoBlockListPropertyValue("grandchildBlocks", grandChildBlockList)
                    .Object
                    )
                );

            var parentBlockList = UmbracoBlockListFactory.CreateBlockListModel(
                UmbracoBlockListFactory.CreateBlock(
                    UmbracoBlockListFactory.CreateContentOrSettings()
                    .SetupUmbracoBlockListPropertyValue("childBlocks", childBlockList)
                    .Object
                    )
                );

            // Act
            var results = BlockListModelExtensions.FindBlocks(parentBlockList, x => x.Content.GetProperty("MyTextProperty") != null).ToList();

            // Assert
            Assert.AreEqual(2, results.Count());
            Assert.Contains(matchingBlock1, results);
            Assert.Contains(matchingBlock2, results);
        }

        [Test]
        public void Block_is_matched_by_model_property()
        {
            var blockList = UmbracoBlockListFactory.CreateBlockListModel(
                UmbracoBlockListFactory.CreateBlock(
                    UmbracoBlockListFactory.CreateContentOrSettings().Object,
                    UmbracoBlockListFactory.CreateContentOrSettings()
                    .SetupUmbracoTextboxPropertyValue(PropertyAliases.ModelProperty, "Field1")
                    .Object
                    )
                );

            // Act
            var result = BlockListModelExtensions.FindBlockByBoundProperty(blockList, "Field1");

            // Assert
            Assert.AreEqual(blockList.First(), result);
        }

        [Test]
        public void Block_is_matched_by_content_type_alias()
        {
            var contentType = new Mock<IPublishedContentType>();
            contentType.Setup(x => x.Alias).Returns("myAlias");
            var blockContent = new Mock<IPublishedElement>();
            blockContent.SetupGet(x => x.ContentType).Returns(contentType.Object);

            var blockList = new BlockListModel(new List<BlockListItem> {
                new BlockListItem(Udi.Create(Constants.UdiEntityType.Element, Guid.NewGuid()), blockContent.Object, null, null)
            });

            // Act
            var result = BlockListModelExtensions.FindBlockByContentTypeAlias(blockList, "myAlias");

            // Assert
            Assert.AreEqual(blockList.First(), result);
        }

        [Test]
        public void OverridableBlockListModel_returns_OverridableBlockListItem()
        {
            var childContent = new Mock<IOverridablePublishedElement>();
            childContent.Setup(x => x.GetProperty("MyTextProperty")).Returns(UmbracoPropertyFactory.CreateTextboxProperty("MyTextProperty", "value"));

            var childBlockItem = new OverridableBlockListItem(
                new BlockListItem(Udi.Create(Constants.UdiEntityType.Element, Guid.NewGuid()), childContent.Object, null, null),
                x => (IOverridablePublishedElement)x
            );

            var childBlockList = new OverridableBlockListModel(new[] { childBlockItem }, null, x => (IOverridablePublishedElement)x);

            var parentContent = new Mock<IOverridablePublishedElement>();
            var parentProperties = new[] { UmbracoPropertyFactory.CreateBlockListProperty("childBlocks", childBlockList) };
            parentContent.SetupGet(x => x.Properties).Returns(parentProperties);
            parentContent.Setup(x => x.GetProperty("childBlocks")).Returns(parentProperties[0]);
            parentContent.Setup(x => x.Value<OverridableBlockListModel>("childBlocks", null, null, It.IsAny<Fallback>(), null)).Returns(childBlockList);

            var parentBlockItem = new OverridableBlockListItem(
                new BlockListItem(Udi.Create(Constants.UdiEntityType.Element, Guid.NewGuid()), parentContent.Object, null, null),
                x => (IOverridablePublishedElement)x
            );
            var parentBlockList = new OverridableBlockListModel(new[] { parentBlockItem }, null, x => (IOverridablePublishedElement)x);


            // Act
            var result = BlockListModelExtensions.FindBlock(parentBlockList, x => x.Content.GetProperty("MyTextProperty") != null);

            // Assert
            Assert.IsInstanceOf<OverridableBlockListItem>(result);
        }
    }
}
