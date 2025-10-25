# RTSP with ContentDotNet
ContentDotNet supports RTSP 1.0 and 2.0 via the [ContentDotNet.Protocols.Rtsp](https://nuget.org/packages/ContentDotNet.Protocols.Rtsp) NuGet package. To enable RTSP support, please install the aforementioned NuGet package.

# Overview
RTSP (Real Time Streaming Protocol) is a popular protocol for controlling streaming media servers in real time. It sends commands to and from server/client, telling it to "play", "pause" or "record" video/audio. It can also tell clients and servers to configure the experience, such as providing authorization/authentication information or sending timestamps.

# Getting started

### Client
Let's start with exploring the Client message model. There is a class in the `ContentDotNet.Protocols.Rtsp` namespace called `RtspClientMessage`, which defines an RTSP message that is sent to the server from the client.

To instantiate it, use the `RtspClientMessageFactory` class and choose the method type. Example:
```cs
RtspClientMessage clientToServerMessage =
	RtspClientMessageFactory.Options("rtsp://server.example.com RTSP/2.0");
```
You can add your own headers using two ways.

**Raw strings**
You can directly provide raw strings for headers.
```cs
clientToServerMessage.HeaderLines.Add("CSeq: 5");
```

**Models**
You can use `DefaultRtspHeaderFactory` as well as RTSP models to use a modelled approach where you set
a few properties and then use the `.ToString()` method to get the RTSP string representation. Example:

```cs
var factory = new DefaultRtspHeaderFactory();
var cseq = factory.Create<IRtspCSeqHeader>();
cseq.SequenceNumber = 5;
string result = $"{cseq.Text}: {cseq}";
clientToServerMessage.HeaderLines.Add(result);
```
That approach is a bit more flexible since you don't encode strings raw.

There's also a way to use method chaining to modify RTSP header properties:
```cs
var cseq = factory.Create<IRtspCSeqHeader>()
	.WithSequenceNumber(5);
```

There's also an "SDP editor" to add SDP messages:
```cs
if (!clientToServerMessage.Sdp.ContainsVLine())
{
	clientToServerMessage.Sdp = clientToServerMessage.Sdp.WithVLine("0");
}
```

> [!NOTE]
> For full SDP support, please see [ContentDotNet.Protocols.Sdp](https://nuget.org/packages/ContentDotNet.Protocols.Sdp).

Now that we explored the messages, let's create the RTSP client. For that, instantiate the `RtspClient` class, passing the input and output Stream instance.

```cs
NetworkStream netStream = ...; // Example: Connects to the Server
var client = new RtspClient(netStream);
```
You can now use it to send a message:
```cs
client.Send(clientToServerMessage);
// or await client.SendAsync
```
Or you can use it to receive a message from the Server:
```cs
RtspServerMessage serverToClientMessage = client.Receive();
// or await client.ReceiveAsync
```

### Server
Exploring messages first! ✉️✨

An RTSP server message is represented thanks to the `RtspServerMessage` class. It doesn't have request lines or method types, and instead, uses a status line. To instantiate it, just create an instance of it.
```cs
var serverToClientMessage = new RtspServerMessage();
```
SDP and Headers and their usages are aforementioned in the Client-To-Server message model.

The status line is represented thanks to the StatusLine property, which is just a `string`:
```cs
serverToClientMessage.StatusLine = ...;
```

Now, instantiate `RtspServer`.
```cs
var rtspServer = new RtspServer();
```
One good thing about the server is that it can actually plug into as many clients as you want - within one instance of the class. That's why the constructor is parameterless - you choose to add them later. Speaking of which, here's an example of adding RTSP clients:
```cs
int client1Index = rtspServer.AddClient(...);
int client2Index = rtspServer.AddClient(...);
int client3Index = rtspServer.AddClient(...);
```

Now, let's say we want to send that message to Client 2 **only**. To do this, use the `SendMessageToClient` method (or `SendMessageToClientAsync` for the async flavor) and specify the message and the index of the client to send to.
```cs
rtspServer.SendMessageToClient(serverToClientMessage, client2Index);
```

Now, if you want to send a single RTSP server message to all clients at once, use `BroadcastMessage` (or `BroadcastMessageAsync` for the async flavor):
```cs
rtspServer.BroadcastMessage(serverToClientMessage);
```
This will send the same message to clients 1, 2 and 3, respectively.

Now, to read the client-side message, use `Receive` (or `ReceiveAsync` for the async flavor). The following example receives the client-side message from Client 3:
```cs
var clientMessageFromClient3 = rtspServer.Receive(client3Index);
```
