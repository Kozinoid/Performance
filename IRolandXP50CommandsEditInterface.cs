namespace SynthLiveMidiController.InstrumentList.Roland.XP50
{
    //===============================  COMPLEX INTERFACES  ==============================
    public interface ISongListStorageSectionInterface : INames, ISongListStoreData, IAdditionalSongTitle, ISongCallbackInterface { }

    public interface IFastListStorageSectionInterface : INames, IFastListStoreData, IFastCallbackInterface { }

    public interface ISongListEditorSectionInterface : INames, ICommandsEditInterface, IPerformancePartData, IAdditionalSongTitle, IPerformanceEditInterface, ISongCallbackInterface { }

    public interface IFastListEditorSectionInterface : INames, ICommandsEditInterface, IPerformancePartData, IFastCallbackInterface { }

    //===============================  BASE INTERFACES  =================================
    // Interface for Song/Fast Edition data
    public interface ICommandsEditInterface
    {
        // Command EFXSource
        // Command&Channel Local Switch
    }

    // Song CallBack Interface
    public interface ISongCallbackInterface
    {
        // CallBack
    }

    // Fast CallBack Interface
    public interface IFastCallbackInterface
    {
        // CallBack
    }

    // Performance Part List Interface
    public interface IPerformancePartData
    {
        // Preset Receive Hold Parameter
        // Lower Key
        // Upper Key
        // Volume
        // Pan
        // Reverb
        // Chorus
        // Patch
        // Octave
    }

    // Interface for Storing SongList Data  (+)
    public interface ISongListStoreData
    {
        // Song Memory Data
        // Song Commands
    }

    // Interface for Storing FastList Data  (+)
    public interface IFastListStoreData
    {
        // Fast Memory Data
        // Fast Commands
    }

    // Song Preset and Commands name
    public interface INames
    {
        // Song/Preset Name
        // Command Name(index)
    }

    // Additional parameters for Edition Performance Common (Song Section)
    public interface IPerformanceEditInterface
    {
        // Performance Title
        // Tempo
    }

    // Addition Song Parameters for Song Section (Edit and Store)
    public interface IAdditionalSongTitle
    {
        // Singer or Athor name
        // Score Key
    }
}

    
