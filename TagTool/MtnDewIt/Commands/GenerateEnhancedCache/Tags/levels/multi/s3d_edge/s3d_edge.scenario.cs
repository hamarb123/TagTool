using TagTool.Cache;
using TagTool.Cache.HaloOnline;
using TagTool.Common;
using TagTool.Tags.Definitions;
using System.IO;
using System.Collections.Generic;

namespace TagTool.MtnDewIt.Commands.GenerateEnhancedCache.Tags 
{
    public class levels_multi_s3d_edge_s3d_edge_scenario : TagFile
    {
        public levels_multi_s3d_edge_s3d_edge_scenario(GameCache cache, GameCacheHaloOnline cacheContext, Stream stream) : base
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
            var tag = GetCachedTag<Scenario>($@"levels\multi\s3d_edge\s3d_edge");
            var scnr = CacheContext.Deserialize<Scenario>(Stream, tag);

            UpdateForgePalette(scnr);

            scnr.EnemyBiasInfluence = new List<Scenario.PlayerSpawnInfluencerBlock>
            {
                new Scenario.PlayerSpawnInfluencerBlock
                {
                    OverrideFullWeightRadius = 10f,
                    OverrideFalloffRadius = 17f,
                    OverrideUpperHeight = 4f,
                    OverrideLowerHeight = -5f,
                    OverrideWeight = -250f,
                },
            };
            scnr.AcousticsPalette = new List<ScenarioStructureBsp.AcousticsPaletteBlock>
            {
                new ScenarioStructureBsp.AcousticsPaletteBlock
                {
                    Name = CacheContext.StringTable.GetStringId($@"amb_cave_dry"),
                    SoundEnvironment = GetCachedTag<SoundEnvironment>($@"sound\dsp_effects\reverbs\templates\cave"),
                    Type = ScenarioStructureBsp.SoundEnvironmentType.InteriorNarrow,
                    ReverbInterpolationSpeed = 2f,
                    AmbienceBackgroundSound = GetCachedTag<SoundLooping>($@"sound\levels\multi\s3d_edge\amb_cave_dry\amb_cave_dry"),
                    AmbienceInterpolationSpeed = 2f,
                },
                new ScenarioStructureBsp.AcousticsPaletteBlock
                {
                    Name = CacheContext.StringTable.GetStringId($@"amb_edge_open_air"),
                    SoundEnvironment = GetCachedTag<SoundEnvironment>($@"sound\dsp_effects\reverbs\templates\mountains"),
                    ReverbInterpolationSpeed = 2f,
                    AmbienceBackgroundSound = GetCachedTag<SoundLooping>($@"sound\levels\multi\s3d_edge\amb_edge_open_air\amb_edge_open_air"),
                    AmbienceInterpolationSpeed = 2f,
                },
                new ScenarioStructureBsp.AcousticsPaletteBlock
                {
                    Name = CacheContext.StringTable.GetStringId($@"amb_sentinel_room"),
                    SoundEnvironment = GetCachedTag<SoundEnvironment>($@"sound\dsp_effects\reverbs\templates\auditorium"),
                    Type = ScenarioStructureBsp.SoundEnvironmentType.InteriorNarrow,
                    ReverbInterpolationSpeed = 2f,
                    AmbienceBackgroundSound = GetCachedTag<SoundLooping>($@"sound\levels\multi\s3d_edge\amb_sentinel_room\amb_sentinel_room"),
                    AmbienceInterpolationSpeed = 2f,
                },
                new ScenarioStructureBsp.AcousticsPaletteBlock
                {
                    Name = CacheContext.StringTable.GetStringId($@"amb_tech_room"),
                    SoundEnvironment = GetCachedTag<SoundEnvironment>($@"sound\dsp_effects\reverbs\templates\hallway"),
                    Type = ScenarioStructureBsp.SoundEnvironmentType.InteriorNarrow,
                    ReverbInterpolationSpeed = 2f,
                    AmbienceBackgroundSound = GetCachedTag<SoundLooping>($@"sound\levels\multi\s3d_edge\amb_tech_room\amb_tech_room"),
                    AmbienceInterpolationSpeed = 2f,
                },
            };
            scnr.SimulationDefinitionTable = null;
            CacheContext.Serialize(Stream, tag, scnr);
        }
    }
}
