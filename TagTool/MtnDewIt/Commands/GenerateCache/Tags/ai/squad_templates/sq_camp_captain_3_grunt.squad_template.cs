using TagTool.Cache;
using TagTool.Cache.HaloOnline;
using TagTool.Common;
using TagTool.Tags.Definitions;
using System.IO;
using System.Collections.Generic;

namespace TagTool.MtnDewIt.Commands.GenerateCache.Tags 
{
    public class ai_squad_templates_sq_camp_captain_3_grunt_squad_template : TagFile
    {
        public ai_squad_templates_sq_camp_captain_3_grunt_squad_template(GameCache cache, GameCacheHaloOnline cacheContext, Stream stream) : base
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
            var tag = GetCachedTag<SquadTemplate>($@"ai\squad_templates\sq_camp_captain_3_grunt");
            var sqtm = CacheContext.Deserialize<SquadTemplate>(Stream, tag);
            sqtm.Name = CacheContext.StringTable.GetStringId($@"sq_camp_cap_grunts_3");
            sqtm.CellTemplates = new List<SquadTemplate.CellTemplate>
            {
                new SquadTemplate.CellTemplate
                {
                    Name = CacheContext.StringTable.GetStringId($@"1_captain"),
                    NormalDiffCount = 1,
                    Characters = new List<SquadTemplate.CellTemplate.ObjectBlock>
                    {
                        new SquadTemplate.CellTemplate.ObjectBlock
                        {
                            Probability = 1,
                        },
                    },
                    InitialWeapons = new List<SquadTemplate.CellTemplate.ObjectBlock>
                    {
                        new SquadTemplate.CellTemplate.ObjectBlock
                        {
                            Object = GetCachedTag<Weapon>($@"objects\weapons\rifle\spike_rifle\spike_rifle"),
                            Probability = 1,
                        },
                        new SquadTemplate.CellTemplate.ObjectBlock
                        {
                            Probability = 1,
                        },
                        new SquadTemplate.CellTemplate.ObjectBlock
                        {
                            DifficultyFlags = SquadTemplate.CellTemplate.DifficultyFlagsValue.Heroic | SquadTemplate.CellTemplate.DifficultyFlagsValue.Legendary,
                            Object = GetCachedTag<Weapon>($@"objects\weapons\rifle\covenant_carbine\covenant_carbine"),
                            Probability = 1,
                        },
                        new SquadTemplate.CellTemplate.ObjectBlock
                        {
                            DifficultyFlags = SquadTemplate.CellTemplate.DifficultyFlagsValue.Heroic | SquadTemplate.CellTemplate.DifficultyFlagsValue.Legendary,
                            Object = GetCachedTag<Weapon>($@"objects\weapons\support_low\brute_shot\brute_shot"),
                            Probability = 1,
                        },
                    },
                },
                new SquadTemplate.CellTemplate
                {
                    Name = CacheContext.StringTable.GetStringId($@"3_grunt"),
                    NormalDiffCount = 3,
                    Characters = new List<SquadTemplate.CellTemplate.ObjectBlock>
                    {
                        new SquadTemplate.CellTemplate.ObjectBlock
                        {
                            Probability = 1,
                        },
                    },
                    InitialWeapons = new List<SquadTemplate.CellTemplate.ObjectBlock>
                    {
                        new SquadTemplate.CellTemplate.ObjectBlock
                        {
                            Object = GetCachedTag<Weapon>($@"objects\weapons\pistol\plasma_pistol\plasma_pistol"),
                            Probability = 3,
                        },
                        new SquadTemplate.CellTemplate.ObjectBlock
                        {
                            Object = GetCachedTag<Weapon>($@"objects\weapons\pistol\needler\needler"),
                            Probability = 1,
                        },
                    },
                },
            };
            CacheContext.Serialize(Stream, tag, sqtm);
        }
    }
}
