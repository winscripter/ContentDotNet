# Codebase
This file is designed for new maintainers/contributors to ContentDotNet to have a better understanding at the ContentDotNet
H.264 extension codebase.

## Folders
In the root folder of the ContentDotNet.Extensions.H264 project, each folder plays a key role.

### Cabac/
This folder contains code related to Context Adaptive Binary Arithmetic Coding (CABAC), one of H.264's entropy coding,
such as decoding bins or binarization.

The Internal folder within this folder contains actual binarization &amp; ctxIdx/ctxIdxInc derivation logic.

### Containers/
This contains utilities that represent array-like value type structures to put less pressure on the Garbage Collector. In essence,
let's say, `ContainerMatrix8x8` - is a struct that contains 64 `uint` fields and uses unsafe code to access a specific field,
essentially providing array-like behavior without performing any GC allocations.

### Helpers/
Contains internal utility code used in the decoding process.

### Internal/
This contains actual decoding logic after parsing the bitstream.

The Decoding folder contains decoding logic, like Intra/Inter prediction, Motion Compensation and Deblocking Filter.

The Encoding folder contains the current encoding prototype.

The Macroblocks folder contains general macroblock-related utilities, as well as implementations for symbols
`MbPartPredMode`, `MbPartWidth`, `MbPartHeight` and `NumSubMbPart` as provided by the H.264 spec.

### Models/
The Models folder contains code related to parsing H.264 from the RBSP and the Bitstream, like NAL units, parameter sets,
and macroblock layers. It contains just the raw syntax elements that are later to be processed.

### Pictures/
Contains reference pictures and the Decoded Picture Buffer (DPB) for use in Inter prediction.

### PredictionMode/
Contains derivation &amp; utilities for macroblock prediction modes.

### Residuals/
Utilities for H.264 residuals.

### Tracks/
NAL units in a simplified way.

### Usability/
Contains code to make working with Video Usability Information (VUI) in H.264 easier.

### Utilities/
Common utility code.
