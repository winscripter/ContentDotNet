# Minimal Types
According to the C# `sizeof` keyword, a `Residual` structure weights ~78KB and `MacroblockLayer` weights over `83KB` memory. That's a lot of stack space to allocate!

Here's the problem though. In H.264, a frame consists of macroblocks, where your frame becomes sort of like a grid with 16x16 blocks. That being said, the frame is often represented via the Slice Data (not to be confused with Slice Header), which, alone, contains all macroblocks. And each macroblock? Is 83KB memory to allocate.
It might not feel like a big deal, but let's just note that a 1920x1080 (Full HD) video, which is basically used everywhere, has `(1920/16)*(1080/16)` macroblocks. **That's 8100 macroblocks!** And with each macroblock allocating 83KB memory, **you'll end up allocating... _656MB memory_, <u>per frame</u>!** Inefficiency taken to the next level.
And we do have to put unprocessed macroblocks in a buffer, since they often rely on neighboring macroblocks, which means to access them, we'll have to do some backtracking and restoration of the CABAC state which is going to be hopelessly slow for just decoding the H.264 video.

Minimal Types are the solution against this problem. They don't contain everything, like raw data to DC/AC coefficients. They contain only what ContentDotNet needs. A bare minimum. These guys are no joke - **they're over >70x more efficient than actual types**, at the cost of removing things like raw syntax elements, providing only what's actually ought to be there.

So yes, this feature isn't made specifically for everyone - e.g. if you're analyzing the syntax elements of your residual, it's better to use the actual type than the minimal type, since minimal types were designed specifically for ContentDotNet, so that .NET apps decoding H.264 videos with ContentDotNet ensure to do so with acceptable memory usage.

And they're compatible too! They rely on actual types. To create a minimal type for Residuals, you need to parse the actual residual first. It might not sound like anything more efficient, but after creating the minimal type, it's the minimal type that's put into the buffer, and the actual type is freed from memory as its values have already been copied to the minimal type.

You can utilize minimal types in the `ContentDotNet.Extensions.H264.Minimal` namespace.
