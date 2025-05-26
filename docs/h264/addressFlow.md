# The AddressFlow Structure
Located under the `ContentDotNet.Extensions.H264.Macroblocks` structure, the `AddressFlow` structure is a handy, zero-allocation structure to traverse through macroblock addresses. For instance, if you want a macroblock to the left-up-up-left-left, you can use the `AddressFlow` structure for that, which provides a convenient fluent interface.

Instantiating it involves passing three parameters. The first one is the initial macroblock address. The second one specifies the picture width in macroblocks. The third one defines total macroblocks in the entire picture.

Methods `Left`, `Up`, `Right`, `Down` return `AddressFlow` and can be used to change the direction of the macroblock address.

Finally, the property `Value` defines the final macroblock address.

So, to compute the left-up-up-left-left macroblock address, you'd use:
```cs
int picWidthInMbs = 50;
int totalMbs = 50 * 10;
var addressFlow = new AddressFlow((picWidthInMbs * 3) - (picWidthInMbs / 2), picWidthInMbs, totalMbs);
int result = addressFlow
             .Left()
             .Up()
             .Up()
             .Left()
             .Left()
             .Value;
```
