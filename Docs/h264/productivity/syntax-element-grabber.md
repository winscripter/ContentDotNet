# Syntax Element Grabber
Let's say you have an `H264RbspState` class and you want to access the boolean `entropy_coding_mode_flag`.

> [!NOTE]
> That one bit is really important, because when it's 1, it's how we know the video is coded under CABAC.
> So if we're inspecting the bitstream for the macroblocks and slice datas, we'll certainly stumble upon
> CABAC values if this bit is 1. It's a use-case for when we'll actually need it.

Now you have multiple options.

The first option would be juggling through multiple inner properties, all of which can be null.
```cs
bool isCabac = rbspState
	.PictureParameterSet
	.EntropyCodingModeFlag;
```
It works, but then we're getting a warning at the `.PictureParameterSet` line, that the value could be null.
That's because it's a nullable property. It's our decision to make that property nullable since the `H264RbspState` class
is designed to store values that were already discovered - so if you haven't yet discovered the PPS NAL unit, that value
would be null.

So, one has two ways to fix this.

The first option is to use the `!` operator to tell the compiler, "hey, I know this is not null!":
```cs
bool isCabac = rbspState
	.PictureParameterSet! // <-- HERE
	.EntropyCodingModeFlag;
```
It works if you've done the same null check somewhere earlier.

> [!WARNING]
> But wait! If PictureParameterSet is actually null? You'll get a NullReferenceException. This exception
> should never occur like this, and instead, should be replaced by more meaningful exceptions.

The other way is to use the `??` operator to tell the compiler, "hey, pick right-side if the left-side is null!".

```cs
bool isCabac = rbspState
	.PictureParameterSet?
	.EntropyCodingModeFlag
	?? false;
```
It works, but what if the `PictureParameterSet` isn't initialized? For instance, if the decoder that's being used
isn't the built-in one and may not initialize the PPS. Oh, and should I mention - that code is quite boilerplate? Could we write something
that costs us less time to type and looks better? That's where Option 2 comes in.

# Option 2: Syntax Element Grabber™️
Well... not really trademarked, but still, the humble "Syntax Element Grabber"™️ is a centralized place to obtain
syntax elements directly from the RBSP H.264 states. That means you can obtain syntax elements from your H.264 RBSP state
not only quickly, but also without writing boilerplate code.

It's located in the `ContentDotNet.Extensions.Video.H264.Utilities` namespace.

To get EntropyCodingModeFlag? Just do:
```cs
bool isCabac = SyntaxElementGrabber
	.GetEntropyCodingModeFlag(rbspState);
```

It fixes all the three aforementioned issues. And if `rbspState` or `PictureParameterSet` is null, it throws an
`InvalidOperationException`, with an actually meaningful exception message.

`SyntaxElementGrabber` can be used for any syntax element under SPS, PPS, and Slice Header.

Aside from `Get...` methods, there's also:
- `Fetch...` that return a nullable value directly:
```cs
bool? isCabac = SyntaxElementGrabber
	.FetchEntropyCodingModeFlag(rbspState);
```
- `TryFetch...` which use a Try pattern:
```cs
bool fetched = SyntaxElementGrabber
	.TryFetchEntropyCodingModeFlag(
		rbspState,
		out bool? isCabac
	)
);
if (fetched)
{
	// ...
}
```
It's a really big class - over 150 methods - but it's worth it!

> Stop juggling nulls. Start grabbing syntax.
>
> - winscripter, Oct 9 2025
