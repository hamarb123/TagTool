using TagTool.Cache;
using TagTool.Cache.HaloOnline;
using TagTool.Common;
using TagTool.Tags.Definitions;
using System.IO;
using System.Collections.Generic;

namespace TagTool.MtnDewIt.Commands.GenerateCache.Tags 
{
    public class objects_characters_dervish_dervish_model_animation_graph : TagFile
    {
        public objects_characters_dervish_dervish_model_animation_graph(GameCache cache, GameCacheHaloOnline cacheContext, Stream stream) : base
        (
            cache,
            cacheContext,
            stream
        )
        {
            Cache = cache;
            CacheContext = cacheContext;
            Stream = stream;
            TagData();
        }

        public override void TagData()
        {
            var tag = GetCachedTag<ModelAnimationGraph>($@"objects\characters\dervish\dervish");
            var jmad = CacheContext.Deserialize<ModelAnimationGraph>(Stream, tag);
            jmad.Modes.Add(new ModelAnimationGraph.Mode
            {
                Name = CacheContext.StringTable.GetStringId("sprint"),
                WeaponClass = new List<ModelAnimationGraph.Mode.WeaponClassBlock>
                {
                    new ModelAnimationGraph.Mode.WeaponClassBlock
                    {
                        Label = CacheContext.StringTable.GetStringId("missile"),
                        WeaponType = new List<ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock>
                        {
                            new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock
                            {
                                Label = CacheContext.StringTable.GetStringId("any"),
                                Set = new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.AnimationSet
                                {
                                    Actions = new List<ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.Entry>
                                    {
                                        new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.Entry
                                        {
                                            Label = CacheContext.StringTable.GetStringId("move_front"),
                                            GraphIndex = 0,
                                            Animation = 1605,
                                        },
                                    },
                                },
                            },
                        },
                    },
                    new ModelAnimationGraph.Mode.WeaponClassBlock
                    {
                        Label = CacheContext.StringTable.GetStringId("rifle"),
                        WeaponType = new List<ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock>
                        {
                            new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock
                            {
                                Label = CacheContext.StringTable.GetStringId("any"),
                                Set = new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.AnimationSet
                                {
                                    Actions = new List<ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.Entry>
                                    {
                                        new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.Entry
                                        {
                                            Label = CacheContext.StringTable.GetStringId("move_front"),
                                            GraphIndex = 0,
                                            Animation = 1607,
                                        },
                                    },
                                },
                            },
                        },
                    },
                    new ModelAnimationGraph.Mode.WeaponClassBlock
                    {
                        Label = CacheContext.StringTable.GetStringId("pistol"),
                        WeaponType = new List<ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock>
                        {
                            new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock
                            {
                                Label = CacheContext.StringTable.GetStringId("any"),
                                Set = new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.AnimationSet
                                {
                                    Actions = new List<ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.Entry>
                                    {
                                        new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.Entry
                                        {
                                            Label = CacheContext.StringTable.GetStringId("move_front"),
                                            GraphIndex = 0,
                                            Animation = 1606,
                                        },
                                    },
                                },
                            },
                        },
                    },
                    new ModelAnimationGraph.Mode.WeaponClassBlock
                    {
                        Label = CacheContext.StringTable.GetStringId("sword"),
                        WeaponType = new List<ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock>
                        {
                            new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock
                            {
                                Label = CacheContext.StringTable.GetStringId("any"),
                                Set = new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.AnimationSet
                                {
                                    Actions = new List<ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.Entry>
                                    {
                                        new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.Entry
                                        {
                                            Label = CacheContext.StringTable.GetStringId("move_front"),
                                            GraphIndex = 0,
                                            Animation = 1608,
                                        },
                                    },
                                },
                            },
                        },
                    },
                    new ModelAnimationGraph.Mode.WeaponClassBlock
                    {
                        Label = CacheContext.StringTable.GetStringId("ball"),
                        WeaponType = new List<ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock>
                        {
                            new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock
                            {
                                Label = CacheContext.StringTable.GetStringId("any"),
                                Set = new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.AnimationSet
                                {
                                    Actions = new List<ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.Entry>
                                    {
                                        new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.Entry
                                        {
                                            Label = CacheContext.StringTable.GetStringId("move_front"),
                                            GraphIndex = 0,
                                            Animation = 1603,
                                        },
                                    },
                                },
                            },
                        },
                    },
                    new ModelAnimationGraph.Mode.WeaponClassBlock
                    {
                        Label = CacheContext.StringTable.GetStringId("hammer"),
                        WeaponType = new List<ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock>
                        {
                            new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock
                            {
                                Label = CacheContext.StringTable.GetStringId("any"),
                                Set = new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.AnimationSet
                                {
                                    Actions = new List<ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.Entry>
                                    {
                                        new ModelAnimationGraph.Mode.WeaponClassBlock.WeaponTypeBlock.Entry
                                        {
                                            Label = CacheContext.StringTable.GetStringId("move_front"),
                                            GraphIndex = 0,
                                            Animation = 1604,
                                        },
                                    },
                                },
                            },
                        },
                    },
                },
            });
            CacheContext.Serialize(Stream, tag, jmad);

            SortModes(tag);
        }
    }
}
