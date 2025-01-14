using TagTool.Common;
using TagTool.Tags;
using System.Runtime.InteropServices;
using System;
using TagTool.Cache;
using TagTool.IO;
using TagTool.Serialization;
using TagTool.Commands.Common;

namespace TagTool.MtnDewIt.BlamFiles
{
    public class BlfData
    {
        public CacheVersion Version;
        public CachePlatform CachePlatform;
        public EndianFormat Format;

        public BlfDataFileContentFlags ContentFlags;
        public BlfDataAuthenticationType AuthenticationType;

        public BlfDataChunkStartOfFile StartOfFile;
        public BlfDataEndOfFileCRC EndOfFileCRC;
        public BlfDataEndOfFileRSA EndOfFileRSA;
        public BlfDataEndOfFileSHA1 EndOfFileSHA1;
        public BlfDataChunkEndOfFile EndOfFile;
        public BlfDataAuthor Author;
        public BlfDataCampaign Campaign;
        public BlfDataScenario Scenario;
        public BlfDataModPackageReference ModReference;
        public BlfDataMapVariantTagNames MapVariantTagNames;
        public BlfDataMapVariant MapVariant;
        public BlfDataGameVariant GameVariant;
        public BlfDataContentHeader ContentHeader;
        public BlfDataMapImage MapImage;
        public byte[] Buffer;

        public BlfData(CacheVersion version, CachePlatform cachePlatform)
        {
            Version = version;
            CachePlatform = cachePlatform;
        }

        public bool ReadData(EndianReader reader) 
        {
            if (!IsValidBlf(reader))
                return false;

            var deserializer = new TagDeserializer(Version, CachePlatform);

            while (!reader.EOF) 
            {
                var dataContext = new DataSerializationContext(reader, useAlignment: false);
                var chunkHeaderPosition = reader.Position;

                var header = deserializer.Deserialize<BlfDataChunkHeader>(dataContext);
                reader.SeekTo(chunkHeaderPosition);

                switch (header.Signature.ToString()) 
                {
                    case "_blf":
                        ContentFlags |= BlfDataFileContentFlags.StartOfFile;
                        StartOfFile = deserializer.Deserialize<BlfDataChunkStartOfFile>(dataContext);
                        break;

                    case "_eof":
                        ContentFlags |= BlfDataFileContentFlags.EndOfFile;
                        var position = reader.Position;
                        EndOfFile = deserializer.Deserialize<BlfDataChunkEndOfFile>(dataContext);
                        AuthenticationType = EndOfFile.AuthenticationType;
                        switch (AuthenticationType)
                        {
                            case BlfDataAuthenticationType.AuthenticationTypeNone:
                                break;
                            case BlfDataAuthenticationType.AuthenticationTypeCRC:
                                reader.SeekTo(position);
                                EndOfFileCRC = deserializer.Deserialize<BlfDataEndOfFileCRC>(dataContext);
                                break;
                            case BlfDataAuthenticationType.AuthenticationTypeRSA:
                                reader.SeekTo(position);
                                EndOfFileRSA = deserializer.Deserialize<BlfDataEndOfFileRSA>(dataContext);
                                break;
                            case BlfDataAuthenticationType.AuthenticationTypeSHA1:
                                reader.SeekTo(position);
                                EndOfFileSHA1 = deserializer.Deserialize<BlfDataEndOfFileSHA1>(dataContext);
                                break;
                        }
                        return true;

                    case "cmpn":
                        ContentFlags |= BlfDataFileContentFlags.Campaign;
                        Campaign = deserializer.Deserialize<BlfDataCampaign>(dataContext);
                        break;

                    case "levl":
                        ContentFlags |= BlfDataFileContentFlags.Scenario;
                        Scenario = deserializer.Deserialize<BlfDataScenario>(dataContext);
                        break;

                    case "modp":
                        ContentFlags |= BlfDataFileContentFlags.ModReference;
                        if (header.MajorVersion == (short)BlfDataModPackageReferenceVersion.Version1)
                        {
                            var v1 = deserializer.Deserialize<BlfDataModPackageReferenceV1>(dataContext);
                            ModReference = new BlfDataModPackageReference(v1);
                        }
                        else
                        {
                            ModReference = deserializer.Deserialize<BlfDataModPackageReference>(dataContext);
                        }
                        break;

                    case "mapv":
                        ContentFlags |= BlfDataFileContentFlags.MapVariant;
                        MapVariant = deserializer.Deserialize<BlfDataMapVariant>(dataContext);
                        break;

                    case "tagn":
                        ContentFlags |= BlfDataFileContentFlags.MapVariantTagNames;
                        MapVariantTagNames = deserializer.Deserialize<BlfDataMapVariantTagNames>(dataContext);
                        break;

                    case "mpvr":
                        ContentFlags |= BlfDataFileContentFlags.GameVariant;
                        GameVariant = deserializer.Deserialize<BlfDataGameVariant>(dataContext);
                        break;

                    case "chdr":
                        ContentFlags |= BlfDataFileContentFlags.ContentHeader;
                        ContentHeader = deserializer.Deserialize<BlfDataContentHeader>(dataContext);
                        break;

                    case "mapi":
                        ContentFlags |= BlfDataFileContentFlags.MapImage;
                        MapImage = deserializer.Deserialize<BlfDataMapImage>(dataContext);
                        Buffer = reader.ReadBytes(MapImage.BufferSize);
                        break;

                    case "scnd":
                    case "scnc":
                    case "flmh":
                    case "flmd":
                    case "athr":
                        Author = deserializer.Deserialize<BlfDataAuthor>(dataContext);
                        break;
                    case "ssig":
                    case "mps2":
                    case "chrt":
                    default:
                        throw new NotImplementedException($"BLF chunk type {header.Signature} not implemented!");
                }
            }

            return true;
        }

        public bool ReadLegacyData(EndianReader reader) 
        {
            if (!IsValidBlf(reader))
                return false;

            var deserializer = new TagDeserializer(Version, CachePlatform);

            while (!reader.EOF)
            {
                var dataContext = new DataSerializationContext(reader, useAlignment: false);
                var chunkHeaderPosition = reader.Position;

                var header = deserializer.Deserialize<BlfDataChunkHeader>(dataContext);
                reader.SeekTo(chunkHeaderPosition);

                switch (header.Signature.ToString())
                {
                    case "_blf":
                        ContentFlags |= BlfDataFileContentFlags.StartOfFile;
                        StartOfFile = deserializer.Deserialize<BlfDataChunkStartOfFile>(dataContext);
                        break;

                    case "_eof":
                        ContentFlags |= BlfDataFileContentFlags.EndOfFile;
                        var position = reader.Position;
                        EndOfFile = deserializer.Deserialize<BlfDataChunkEndOfFile>(dataContext);
                        AuthenticationType = EndOfFile.AuthenticationType;
                        switch (AuthenticationType)
                        {
                            case BlfDataAuthenticationType.AuthenticationTypeNone:
                                break;
                            case BlfDataAuthenticationType.AuthenticationTypeCRC:
                                reader.SeekTo(position);
                                EndOfFileCRC = deserializer.Deserialize<BlfDataEndOfFileCRC>(dataContext);
                                break;
                            case BlfDataAuthenticationType.AuthenticationTypeRSA:
                                reader.SeekTo(position);
                                EndOfFileRSA = deserializer.Deserialize<BlfDataEndOfFileRSA>(dataContext);
                                break;
                            case BlfDataAuthenticationType.AuthenticationTypeSHA1:
                                reader.SeekTo(position);
                                EndOfFileSHA1 = deserializer.Deserialize<BlfDataEndOfFileSHA1>(dataContext);
                                break;
                        }
                        return true;

                    case "cmpn":
                        ContentFlags |= BlfDataFileContentFlags.Campaign;
                        Campaign = deserializer.Deserialize<BlfDataCampaign>(dataContext);
                        break;

                    case "levl":
                        ContentFlags |= BlfDataFileContentFlags.Scenario;
                        reader.Format = EndianFormat.LittleEndian;
                        Scenario = deserializer.Deserialize<BlfDataScenario>(dataContext);
                        reader.Format = Format;
                        Scenario.Signature = new Tag("levl");
                        Scenario.Length = (int)TagStructure.GetStructureSize(typeof(BlfDataScenario), CacheVersion.HaloOnline106708, CachePlatform.Original);
                        Scenario.MajorVersion = 3;
                        Scenario.MinorVersion = 1;
                        break;

                    case "mapv":
                        ContentFlags |= BlfDataFileContentFlags.MapVariant;
                        reader.Format = EndianFormat.LittleEndian;
                        MapVariant = deserializer.Deserialize<BlfDataMapVariant>(dataContext);
                        reader.Format = Format;
                        MapVariant.Signature = new Tag("mapv");
                        MapVariant.Length = (int)TagStructure.GetStructureSize(typeof(BlfDataMapVariant), CacheVersion.HaloOnline106708, CachePlatform.Original);
                        MapVariant.MajorVersion = 12;
                        MapVariant.MinorVersion = 1;
                        break;

                    case "mpvr":
                        ContentFlags |= BlfDataFileContentFlags.GameVariant;
                        reader.Format = EndianFormat.LittleEndian;
                        GameVariant = deserializer.Deserialize<BlfDataGameVariant>(dataContext);
                        reader.Format = Format;
                        GameVariant.Signature = new Tag("mpvr");
                        GameVariant.Length = (int)TagStructure.GetStructureSize(typeof(BlfDataGameVariant), CacheVersion.HaloOnline106708, CachePlatform.Original);
                        GameVariant.MajorVersion = 3;
                        GameVariant.MinorVersion = 1;
                        break;

                    case "chdr":
                        ContentFlags |= BlfDataFileContentFlags.ContentHeader;
                        reader.Format = EndianFormat.LittleEndian;
                        ContentHeader = deserializer.Deserialize<BlfDataContentHeader>(dataContext);
                        reader.Format = Format;
                        ContentHeader.Signature = new Tag("chdr");
                        ContentHeader.Length = (int)TagStructure.GetStructureSize(typeof(BlfDataContentHeader), CacheVersion.HaloOnline106708, CachePlatform.Original);
                        ContentHeader.MajorVersion = 9;
                        ContentHeader.MinorVersion = 3;
                        break;

                    case "mapi":
                        ContentFlags |= BlfDataFileContentFlags.MapImage;
                        MapImage = deserializer.Deserialize<BlfDataMapImage>(dataContext);
                        Buffer = reader.ReadBytes(MapImage.BufferSize);
                        break;

                    case "scnd":
                    case "scnc":
                    case "flmh":
                    case "flmd":
                    case "athr":
                        Author = deserializer.Deserialize<BlfDataAuthor>(dataContext);
                        break;
                    case "ssig":
                    case "mps2":
                    case "chrt":
                    default:
                        throw new NotImplementedException($"BLF chunk type {header.Signature} not implemented!");
                }
            }

            return true;
        }

        public bool WriteData(EndianWriter writer) 
        {
            if (!ContentFlags.HasFlag(BlfDataFileContentFlags.StartOfFile) || !ContentFlags.HasFlag(BlfDataFileContentFlags.EndOfFile))
                return false;

            TagSerializer serializer = new TagSerializer(Version, CachePlatform, Format);
            writer.Format = Format;
            var dataContext = new DataSerializationContext(writer, useAlignment: false);

            serializer.Serialize(dataContext, StartOfFile);

            if (ContentFlags.HasFlag(BlfDataFileContentFlags.Scenario))
                serializer.Serialize(dataContext, Scenario);

            if (ContentFlags.HasFlag(BlfDataFileContentFlags.ContentHeader))
                serializer.Serialize(dataContext, ContentHeader);

            if (ContentFlags.HasFlag(BlfDataFileContentFlags.MapVariant))
                serializer.Serialize(dataContext, MapVariant);

            if (ContentFlags.HasFlag(BlfDataFileContentFlags.GameVariant))
                serializer.Serialize(dataContext, GameVariant);

            if (ContentFlags.HasFlag(BlfDataFileContentFlags.Campaign))
                serializer.Serialize(dataContext, Campaign);

            if (ContentFlags.HasFlag(BlfDataFileContentFlags.ModReference))
                serializer.Serialize(dataContext, ModReference);

            if (ContentFlags.HasFlag(BlfDataFileContentFlags.MapVariantTagNames))
                serializer.Serialize(dataContext, MapVariantTagNames);

            if (ContentFlags.HasFlag(BlfDataFileContentFlags.MapImage))
            {
                if (Buffer != null && Buffer.Length > 0)
                {
                    MapImage.BufferSize = Buffer.Length;
                    serializer.Serialize(dataContext, MapImage);

                    // image is always little endian
                    writer.Format = EndianFormat.LittleEndian;
                    writer.WriteBlock(Buffer);
                    writer.Format = Format;
                }
                else
                {
                    new TagToolError(CommandError.CustomError, "No data, image will not be written to BLF");
                }
            }


            switch (AuthenticationType)
            {
                case BlfDataAuthenticationType.AuthenticationTypeNone:
                    serializer.Serialize(dataContext, EndOfFile);
                    break;
                case BlfDataAuthenticationType.AuthenticationTypeCRC:
                    serializer.Serialize(dataContext, EndOfFileCRC);
                    break;
                case BlfDataAuthenticationType.AuthenticationTypeRSA:
                    serializer.Serialize(dataContext, EndOfFileRSA);
                    break;
                case BlfDataAuthenticationType.AuthenticationTypeSHA1:
                    serializer.Serialize(dataContext, EndOfFileSHA1);
                    break;
            }

            return true;
        }

        private bool IsValidBlf(EndianReader reader)
        {
            var position = reader.Position;

            try
            {
                Format = FindChunkEndianFormat(reader);
                reader.Format = Format;
            }
            catch (Exception e)
            {
                Console.WriteLine($"BLF file is invalid: {e.Message}");
                return false;
            }

            var deserializer = new TagDeserializer(Version, CachePlatform);
            var dataContext = new DataSerializationContext(reader, useAlignment: false);

            var header = deserializer.Deserialize<BlfDataChunkHeader>(dataContext);
            reader.SeekTo(position);

            if (header.Signature == "_blf")
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        private EndianFormat FindChunkEndianFormat(EndianReader reader)
        {
            reader.Format = EndianFormat.LittleEndian;
            var startOfFile = reader.Position;
            var chunkHeaderSize = (int)TagStructure.GetTagStructureInfo(typeof(BlfDataChunkHeader), Version, CachePlatform).TotalSize;
            reader.SeekTo(startOfFile + chunkHeaderSize);

            if (reader.ReadInt16() == -2)
            {
                reader.SeekTo(startOfFile);
                return EndianFormat.LittleEndian;
            }
            else
            {
                reader.SeekTo(startOfFile + chunkHeaderSize);
                reader.Format = EndianFormat.BigEndian;

                if (reader.ReadInt16() == -2)
                {
                    reader.SeekTo(startOfFile);
                    return EndianFormat.BigEndian;
                }
                else
                {
                    reader.SeekTo(startOfFile);
                    throw new Exception("Invalid BOM found in BLF start of file chunk");
                }
            }
        }
    }

    [Flags]
    public enum BlfDataFileContentFlags : uint
    {
        None = 0,
        StartOfFile = 1 << 0,
        EndOfFile = 1 << 1,
        ContentHeader = 1 << 2,
        MapVariant = 1 << 3,
        Scenario = 1 << 4,
        Campaign = 1 << 5,
        GameVariant = 1 << 6,
        ModReference = 1 << 7,
        MapVariantTagNames = 1 << 8,
        MapImage = 1 << 9,
    }

    [TagStructure(Size = 0xC, Align = 0x1)]
    public class BlfDataChunkHeader
    {
        public Tag Signature;
        public int Length;
        public short MajorVersion;
        public short MinorVersion;
    }

    [TagStructure(Size = 0x24, Align = 0x1)]
    public class BlfDataChunkStartOfFile : BlfDataChunkHeader
    {
        public short ByteOrderMarker;

        [TagField(Length = 0x20)]
        public string InternalName;
        
        [TagField(Length = 2, Flags = TagFieldFlags.Padding)]
        public byte[] Padding;
    }

    public enum BlfDataAuthenticationType : byte
    {
        AuthenticationTypeNone,
        AuthenticationTypeCRC,
        AuthenticationTypeSHA1,
        AuthenticationTypeRSA,
    }

    [TagStructure(Size = 0x4, Align = 0x1, MinVersion = CacheVersion.HaloOnline106708, MaxVersion = CacheVersion.HaloOnline106708)]
    [TagStructure(Size = 0x5, Align = 0x1)]
    public class BlfDataChunkEndOfFile : BlfDataChunkHeader 
    {
        public int AuthenticationDataSize;

        [TagField(MaxVersion = CacheVersion.HaloOnlineED)]
        public BlfDataAuthenticationType AuthenticationType;
    }

    [TagStructure(Size = 0x4, Align = 0x1)]
    public class BlfDataEndOfFileCRC : BlfDataChunkEndOfFile
    {
        public uint Checksum;
    }

    [TagStructure(Size = 0x100, Align = 0x1)]
    public class BlfDataEndOfFileSHA1 : BlfDataChunkEndOfFile
    {
        [TagField(Length = 0x100)]
        public byte[] Hash;
    }

    [TagStructure(Size = 0x100)]
    public class RSASignature
    {
        [TagField(Length = 0x100)]
        public byte[] Data;
    }

    [TagStructure(Size = 0x100, Align = 0x1)]
    public class BlfDataEndOfFileRSA : BlfDataChunkEndOfFile
    {
        public RSASignature RSASignature;
    }

    [TagStructure(Size = 0x44, Align = 0x1)]
    public class BlfDataAuthor : BlfDataChunkHeader 
    {
        [TagField(Length = 16)]
        public string BuildName;

        public ulong BuildIdentifier;

        [TagField(Length = 28)]
        public string BuildString;

        [TagField(Length = 16)]
        public string Author;
    }

    public enum BlfContentItemType : int
    {
        None = -1,
        GameState,
        CtfVariant,
        SlayerVariant,
        OddballVariant,
        KingOfTheHillVariant,
        JuggernautVariant,
        TerritoriesVariant,
        AssaultVariant,
        InfectionVariant,
        VipVariant,
        SandboxMap,
        Film,
        FilmClip,
        ScreenShot,
    }

    public enum BlfGameEngineType : int
    {
        None,
        CaptureTheFlag,
        Slayer,
        Oddball,
        KingOfTheHill,
        Sandbox,
        Vip,
        Juggernaut,
        Territories,
        Assault,
        Infection,
    }

    [TagStructure(Size = 0xF8)]
    public class BlfContentItemMetadata : TagStructure 
    {
        public ulong Identifier;

        [TagField(CharSet = CharSet.Unicode, Length = 16)]
        public string Name = string.Empty;

        [TagField(CharSet = CharSet.Ansi, Length = 128)]
        public string Description = string.Empty;

        [TagField(CharSet = CharSet.Ansi, Length = 16)]
        public string Author = string.Empty;

        public BlfContentItemType ContentType;
        public bool UserIsOnline;

        [TagField(Flags = TagFieldFlags.Padding, Length = 3)]
        public byte[] Padding1 = new byte[3];

        public ulong UserId;
        public ulong ContentSize;
        public ulong Timestamp;
        public int FilmDuration;
        public int CampaignId = -1;
        public int MapId = -1;
        public BlfGameEngineType GameEngineType;
        public int CampaignDifficulty = -1;
        public sbyte CampaignInsertionPoint = -1;
        public bool IsSurvival;

        [TagField(Flags = TagFieldFlags.Padding, Length = 2)]
        public byte[] Padding2 = new byte[2];

        public ulong GameId;
    }

    [TagStructure(Size = 0xFC, Align = 0x1)]
    public class BlfDataContentHeader : BlfDataChunkHeader 
    {
        public ushort BuildVersion;
        public ushort MapMinorVersion;
        public BlfContentItemMetadata Metadata;
    }

    [TagStructure(Size = 0x264, Align = 0x1)]
    public class BlfDataGameVariant : BlfDataChunkHeader
    {
        public GameVariantData GameVariant;
    }

    [TagStructure(Size = 0xE094, Align = 0x1)]
    public class BlfDataMapVariant : BlfDataChunkHeader
    {
        public uint VariantVersion;
        public MapVariantData MapVariant;
    }

    [TagStructure(Size = 0x80, Align = 0x1)]
    public class NameUnicode64
    {
        [TagField(CharSet = CharSet.Unicode, Length = 0x40, DataAlign = 0x1)]
        public string Name;
    }

    [TagStructure(Size = 0x100, Align = 0x1)]
    public class NameUnicode128
    {
        [TagField(CharSet = CharSet.Unicode, Length = 0x80, DataAlign = 0x1)]
        public string Name;
    }

    [TagStructure(Size = 0x130C, Align = 0x1)]
    public class BlfDataCampaign : BlfDataChunkHeader 
    {
        public int CampaignId;
        public uint Type;

        [TagField(Length = 0xC)]
        public NameUnicode64[] Names;

        [TagField(Length = 0xC)]
        public NameUnicode128[] Descriptions;

        [TagField(Length = 0x40)]
        public int[] MapIds;

        [TagField(Length = 4, Flags = TagFieldFlags.Padding)]
        public byte[] Padding1;
    }

    public enum BlfDataScenarioInsertionFlags : byte
    {
        None = 0,
        SurvivalBit1,
        Bit2,
        SurvivalBit2,
        Bit3,
        Bit4,
        Bit6,
        Bit5,
        FlashbackBit,
    }

    [TagStructure(Size = 0x40, Align = 0x1)]
    public class NameUnicode32
    {
        [TagField(CharSet = CharSet.Unicode, Length = 0x20, DataAlign = 0x1)]
        public string Name;
    }

    [TagStructure(Size = 0xF08, Align = 0x1, MaxVersion = CacheVersion.Halo3Retail)]
    [TagStructure(Size = 0xF10, Align = 0x1, MinVersion = CacheVersion.Halo3ODST, MaxVersion = CacheVersion.HaloOnline700123)]
    public class BlfDataScenarioInsertion 
    {
        public bool Visible;
        public BlfDataScenarioInsertionFlags Flags;
        public short ZoneSetIndex;

        [TagField(MinVersion = CacheVersion.Halo3ODST, MaxVersion = CacheVersion.HaloOnline700123)]
        public int FlashbackMapId;

        [TagField(MinVersion = CacheVersion.Halo3ODST, MaxVersion = CacheVersion.HaloOnline700123)]
        public int SurvivalIndex;

        [TagField(Length = 4, Flags = TagFieldFlags.Padding)]
        public byte[] Padding1;

        [TagField(Length = 0xC)]
        public NameUnicode32[] Names;

        [TagField(Length = 0xC)]
        public NameUnicode128[] Descriptions;
    }

    [Flags]
    public enum BlfDataScenarioFlags : uint
    {
        None = 0,
        Bit1 = 1 << 0,
        Bit2 = 1 << 1,
        Visible = 1 << 2,
        GeneratesFilm = 1 << 3,
        IsMainmenu = 1 << 4,
        IsCampaign = 1 << 5,
        IsMultiplayer = 1 << 6,
        IsDlc = 1 << 7,
        TestBit = 1 << 8,
        TempBit = 1 << 9,
        IsFirefight = 1 << 10,
        IsCinematic = 1 << 11,
        IsForgeOnly = 1 << 12,
    }

    [TagStructure(Size = 0xB)]
    public class BlfDataGameEngineTeams 
    {
        public byte NoGametypeTeamCount;
        public byte OddballTeamCount;
        public byte VipTeamCount;
        public byte AssaultTeamCount;
        public byte CtfTeamCount;
        public byte KothTeamCount;
        public byte JuggernautTeamCount;
        public byte InfectionTeamCount;
        public byte SlayerTeamCount;
        public byte ForgeTeamCount;
        public byte TerritoriesTeamCount;
    }

    [TagStructure(Size = 0x4D44, Align = 0x1, MaxVersion = CacheVersion.Halo3Retail)]
    [TagStructure(Size = 0x89A4, Align = 0x1, MinVersion = CacheVersion.HaloOnline106708, MaxVersion = CacheVersion.HaloOnline106708)]
    [TagStructure(Size = 0x98B4, Align = 0x1, MinVersion = CacheVersion.Halo3ODST, MaxVersion = CacheVersion.HaloOnline700123)]
    public class BlfDataScenario : BlfDataChunkHeader 
    {
        public int MapId;
        public BlfDataScenarioFlags MapFlags;

        [TagField(Length = 0xC)]
        public NameUnicode32[] Names;

        [TagField(Length = 0xC)]
        public NameUnicode128[] Descriptions;

        [TagField(Length = 0x100)]
        public string ImageName;

        [TagField(Length = 0x100)]
        public string MapName;

        public int MapIndex;
        public int GuiSelectableItemType;
        public byte MinimumDesiredPlayers;
        public byte MaximumDesiredPlayers;

        public BlfDataGameEngineTeams GameEngineTeamCounts;

        public bool AllowSavedFilms;

        [TagField(Length = 2, Flags = TagFieldFlags.Padding)]
        public byte[] Padding1;

        [TagField(Length = 4, Flags = TagFieldFlags.Padding)]
        public byte[] Padding2;

        [TagField(Length = 0x4, MaxVersion = CacheVersion.Halo3Retail)]
        [TagField(Length = 0x8, MinVersion = CacheVersion.HaloOnline106708, MaxVersion = CacheVersion.HaloOnline106708)]
        [TagField(Length = 0x9, MinVersion = CacheVersion.Halo3ODST, MaxVersion = CacheVersion.HaloOnline700123)]
        public BlfDataScenarioInsertion[] Insertions;
    }

    public enum BlfDataImageType : uint 
    {
        JPG = 0,
        PNG,
    }

    [TagStructure(Size = 0x8, Align = 0x1)]
    public class BlfDataMapImage : BlfDataChunkHeader
    {
        public BlfDataImageType Type;
        public int BufferSize;
    }


    // EVERYTHING BELOW HERE IS 0.7 SPECIFIC

    [TagStructure(Size = 0x100, Align = 0x1)]
    public class BlfTagName
    {
        [TagField(Length = 0x100)]
        public string Name;
    }
    
    [TagStructure(Size = 0x10000, Align = 0x1)]
    public class BlfDataMapVariantTagNames : BlfDataChunkHeader
    {
        [TagField(Length = 0x100)]
        public BlfTagName[] Names;
    }

    [TagStructure(Size = 0x44, Align = 0x1)]
    public class BlfDataModPackageReferenceV1 : BlfDataChunkHeader
    {
        [TagField(Length = 0x10, CharSet = CharSet.Unicode)]
        public string Name;
        [TagField(Length = 0x10)]
        public string Author;
        [TagField(Length = 0x14)]
        public byte[] Hash;
    }

    enum BlfDataModPackageReferenceVersion : short
    {
        Version1 = 1,
        Current = 2
    }

    [TagStructure(Size = 0x484)]
    public class BlfDataModPackageReference : BlfDataChunkHeader
    {
        [TagField(Length = 0x14)]
        public byte[] Hash;

        public ModPackageMetadata Metadata;

        public BlfDataModPackageReference()
        {
            Signature = new Tag("modp");
            Length = (int)typeof(BlfDataModPackageReference).GetSize() - (int)typeof(BlfDataChunkHeader).GetSize();
            MajorVersion = (short)BlfDataModPackageReferenceVersion.Current;
            MinorVersion = 0;
        }

        public BlfDataModPackageReference(BlfDataModPackageReferenceV1 v1) : this()
        {
            Hash = v1.Hash;
            Metadata = new ModPackageMetadata
            {
                Name = v1.Name,
                Author = v1.Author
            };
        }
    }
}