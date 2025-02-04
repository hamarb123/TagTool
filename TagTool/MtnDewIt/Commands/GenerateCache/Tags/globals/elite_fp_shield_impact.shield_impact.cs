using TagTool.Cache;
using TagTool.Cache.HaloOnline;
using TagTool.Common;
using TagTool.Tags.Definitions;
using System.IO;

namespace TagTool.MtnDewIt.Commands.GenerateCache.Tags 
{
    public class globals_elite_fp_shield_impact_shield_impact : TagFile
    {
        public globals_elite_fp_shield_impact_shield_impact(GameCache cache, GameCacheHaloOnline cacheContext, Stream stream) : base
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
            var tag = GetCachedTag<ShieldImpact>($@"globals\elite_fp_shield_impact");
            var shit = CacheContext.Deserialize<ShieldImpact>(Stream, tag);
            shit.Version = 4;
            shit.ShieldIntensity = new ShieldImpact.ShieldIntensityBlock
            {
                RecentDamageIntensity = 0.15f,
            };
            shit.ShieldEdge = new ShieldImpact.ShieldEdgeBlock
            {
                DepthFadeRange = 0.25f,
                OuterFadeRadius = 0.5f,
                CenterRadius = 0.75f,
                InnerFadeRadius = 1f,
                EdgeGlowColor = new ShieldImpactFunction
                {
                    Function = new TagTool.Tags.TagFunction
                    {
                        Data = new byte[]
                        {
                            0x08, 0x34, 0x02, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00,
                            0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x1C, 0x80, 0xE5, 0xFF,
                            0x6F, 0x12, 0x83, 0x3A, 0x6F, 0x12, 0x03, 0x3B, 0x1C, 0x00,
                            0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0xCD,
                            0xFF, 0xFF, 0x7F, 0x7F, 0xED, 0xBE, 0x6B, 0x3F, 0x9E, 0x33,
                            0x2A, 0xC0, 0xE3, 0x43, 0x2F, 0x40, 0x00, 0x00, 0x00, 0x00,
                        },
                    },
                },
                EdgeGlowIntensity = new ShieldImpactFunction
                {
                    InputVariable = CacheContext.StringTable.GetStringId($@"shield_intensity"),
                    Function = new TagTool.Tags.TagFunction
                    {
                        Data = new byte[]
                        {
                            0x01, 0x34, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00,
                            0xC0, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x6F, 0x12, 0x83, 0x3A, 0x6F, 0x12, 0x03, 0x3B, 0x1C, 0x00,
                            0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0xCD,
                            0xFF, 0xFF, 0x7F, 0x7F, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00,
                            0x40, 0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F,
                        },
                    },
                },
            };
            shit.Plasma = new ShieldImpact.PlasmaBlock
            {
                PlasmaDepthFadeRange = 0.05f,
                PlasmaNoiseBitmap1 = GetCachedTag<Bitmap>($@"levels\shared\bitmaps\test_maps\cloud_1"),
                PlasmaNoiseBitmap2 = GetCachedTag<Bitmap>($@"levels\shared\bitmaps\test_maps\cloud_2"),
                TilingScale = 3f,
                ScrollSpeed = 2f,
                EdgeSharpness = 20f,
                CenterSharpness = 60f,
                PlasmaCenterRadius = 0.5f,
                PlasmaInnerFadeRadius = 0.75f,
                PlasmaCenterColor = new ShieldImpactFunction
                {
                    InputVariable = CacheContext.StringTable.GetStringId($@"shield_intensity"),
                    Function = new TagTool.Tags.TagFunction
                    {
                        Data = new byte[]
                        {
                            0x08, 0x34, 0x02, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00,
                            0x00, 0xFF, 0x00, 0x00, 0x00, 0x00, 0xDE, 0x4B, 0x35, 0xFF,
                            0x00, 0x00, 0xA0, 0x42, 0x00, 0x00, 0x00, 0x00, 0x1C, 0x00,
                            0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0xCD,
                            0xFF, 0xFF, 0x7F, 0x7F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0xA0, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        },
                    },
                },
                PlasmaCenterIntensity = new ShieldImpactFunction
                {
                    InputVariable = CacheContext.StringTable.GetStringId($@"shield_intensity"),
                    Function = new TagTool.Tags.TagFunction
                    {
                        Data = new byte[]
                        {
                            0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00,
                        },
                    },
                },
                PlasmaEdgeColor = new ShieldImpactFunction
                {
                    Function = new TagTool.Tags.TagFunction
                    {
                        Data = new byte[]
                        {
                            0x08, 0x34, 0x02, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x17, 0x39,
                            0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF,
                            0x6F, 0x12, 0x83, 0x3A, 0x6F, 0x12, 0x03, 0x3B, 0x34, 0x00,
                            0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0xCD,
                            0xAE, 0x47, 0xA1, 0x3E, 0x12, 0x25, 0x6D, 0xC0, 0x81, 0xCC,
                            0xF7, 0xC0, 0xC5, 0x68, 0xBF, 0x40, 0x00, 0x00, 0x00, 0x00,
                            0x07, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x7F, 0x7F, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x80, 0x3F,
                        },
                    },
                },
                PlasmaEdgeIntensity = new ShieldImpactFunction
                {
                    InputVariable = CacheContext.StringTable.GetStringId($@"shield_intensity"),
                    Function = new TagTool.Tags.TagFunction
                    {
                        Data = new byte[]
                        {
                            0x08, 0x34, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00,
                            0x00, 0x4F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1C, 0x00,
                            0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0xCD,
                            0xFF, 0xFF, 0x7F, 0x7F, 0x10, 0xC6, 0x62, 0xC0, 0x10, 0xC6,
                            0x22, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F,
                        },
                    },
                },
            };
            shit.ExtrusionOscillation = new ShieldImpact.ExtrusionOscillationBlock
            {
                OscillationBitmap1 = GetCachedTag<Bitmap>($@"levels\shared\bitmaps\test_maps\cloud_1"),
                OscillationBitmap2 = GetCachedTag<Bitmap>($@"levels\shared\bitmaps\test_maps\cloud_2"),
                OscillationTilingScale = 2f,
                OscillationScrollSpeed = 44f,
                ExtrusionAmount = new ShieldImpactFunction
                {
                    Function = new TagTool.Tags.TagFunction
                    {
                        Data = new byte[]
                        {
                            0x08, 0x34, 0x00, 0x00, 0xA6, 0x9B, 0x22, 0x3B, 0xA6, 0x9B,
                            0xC4, 0x3B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1C, 0x00,
                            0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0xCD,
                            0xFF, 0xFF, 0x7F, 0x7F, 0x30, 0xFB, 0xBB, 0xBE, 0x98, 0xFD,
                            0xDD, 0x3F, 0x66, 0x7F, 0x17, 0xC0, 0x00, 0x00, 0x80, 0x3F,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00,
                        },
                    },
                },
                OscillationAmplitude = new ShieldImpactFunction
                {
                    Function = new TagTool.Tags.TagFunction
                    {
                        Data = new byte[]
                        {
                            0x01, 0x34, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00,
                        },
                    },
                },
            };
            shit.HitResponse = new ShieldImpact.HitResponseBlock
            {
                HitTime = 2.857143f,
                HitColor = new ShieldImpactFunction
                {
                    Function = new TagTool.Tags.TagFunction
                    {
                        Data = new byte[]
                        {
                            0x08, 0x35, 0x02, 0x00, 0x39, 0x8A, 0xFF, 0xFF, 0xB4, 0x2A,
                            0x28, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF,
                            0x6F, 0x12, 0x83, 0x3A, 0x6F, 0x12, 0x03, 0x3B, 0x30, 0x00,
                            0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0xCD,
                            0xFF, 0xFF, 0x7F, 0x7F, 0xAB, 0x62, 0xA7, 0xBF, 0x5E, 0x16,
                            0x08, 0x40, 0x73, 0xAF, 0x39, 0x3E, 0x00, 0x00, 0x00, 0x00,
                            0x01, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0xCD, 0xFF, 0xFF,
                            0x7F, 0x7F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F,
                        },
                    },
                },
                HitIntensity = new ShieldImpactFunction
                {
                    Function = new TagTool.Tags.TagFunction
                    {
                        Data = new byte[]
                        {
                            0x01, 0x34, 0x00, 0x00, 0xCD, 0xCC, 0xCC, 0x3D, 0xCD, 0xCC,
                            0x4C, 0x3E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x6F, 0x12, 0x83, 0x3A, 0x6F, 0x12, 0x03, 0x3B, 0x2C, 0x00,
                            0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0xCD,
                            0x5C, 0x8F, 0x02, 0x3F, 0x60, 0x17, 0x07, 0xC0, 0x19, 0xED,
                            0xBF, 0x40, 0x0F, 0x0F, 0x8F, 0xC0, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x7F, 0x7F, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                        },
                    },
                },
            };
            shit.EdgeScales = new RealQuaternion(3f, -4f, 2f, -4f);
            shit.EdgeOffsets = new RealQuaternion(-2f, 0f, 0f, 10f);
            shit.PlasmaScales = new RealQuaternion(5f, 4f, -45f, 45f);
            shit.DepthFadeParameters = new RealQuaternion(4f, 1E-12f, 20f, 20f);
            CacheContext.Serialize(Stream, tag, shit);
        }
    }
}
