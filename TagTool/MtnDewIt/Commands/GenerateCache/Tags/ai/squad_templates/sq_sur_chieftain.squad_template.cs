using TagTool.Cache;
using TagTool.Cache.HaloOnline;
using TagTool.Common;
using TagTool.Tags.Definitions;
using System.IO;
using System.Collections.Generic;

namespace TagTool.MtnDewIt.Commands.GenerateCache.Tags 
{
    public class ai_squad_templates_sq_sur_chieftain_squad_template : TagFile
    {
        public ai_squad_templates_sq_sur_chieftain_squad_template(GameCache cache, GameCacheHaloOnline cacheContext, Stream stream) : base
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
            var tag = GetCachedTag<SquadTemplate>($@"ai\squad_templates\sq_sur_chieftain");
            var sqtm = CacheContext.Deserialize<SquadTemplate>(Stream, tag);
            sqtm.Name = CacheContext.StringTable.GetStringId($@"sq_sur_chieftain");
            sqtm.CellTemplates = new List<SquadTemplate.CellTemplate>
            {
                new SquadTemplate.CellTemplate
                {
                    Name = CacheContext.StringTable.GetStringId($@"1_hammer"),
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
                            Object = GetCachedTag<Weapon>($@"objects\weapons\melee\gravity_hammer\gravity_hammer"),
                            Probability = 1,
                        },
                    },
                },
                new SquadTemplate.CellTemplate
                {
                    Name = CacheContext.StringTable.GetStringId($@"1_plasma"),
                    RoundRange = new Bounds<short>(2, 10),
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
                            Object = GetCachedTag<Weapon>($@"objects\weapons\turret\plasma_cannon\plasma_cannon"),
                            Probability = 1,
                        },
                    },
                },
                new SquadTemplate.CellTemplate
                {
                    Name = CacheContext.StringTable.GetStringId($@"1_flak"),
                    RoundRange = new Bounds<short>(4, 10),
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
                            Object = GetCachedTag<Weapon>($@"objects\weapons\support_high\flak_cannon\flak_cannon"),
                            Probability = 1,
                        },
                    },
                },
            };
            CacheContext.Serialize(Stream, tag, sqtm);
        }
    }
}
