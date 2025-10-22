# Loop Executors
Loop executors can be a way to abstract away the code behind executing a loop that could potentially execute in parallel.

Code that may not execute in parallel involve the following examples:
- Reading data from a file (race conditions can corrupt data)
- Incrementing integers (race conditions here can cause lost updates)

Code that may execute in parallel involve the following examples:
- Performing mathematical operations on large arrays
- Invoking functions that don't depend on variables

## API
Namespace: `ContentDotNet.LoopExecutors`

Assembly: `ContentDotNet`

<hr />

The ILoopExecutor interface defines the contract for loop executors.
```cs
public interface ILoopExecutor
{
	void For(int minInclusive, int maxInclusive, Action<int> body);
	void ForEach<T>(IEnumerable<T> source, Action<T> body);
}
```

They are identical to implementations of the `Parallel` class in .NET, except they are abstracted away behind an interface.

## SimpleLoopExecutor
This implementation uses a simple `for loop` to execute the body.

## ParallelLoopExecutor
This implementation uses `Parallel.For` and `Parallel.ForEach` to execute the body in parallel.

# Benefits and choosing an executor
| Executor | Good for performance? | Supported on singlethreaded environments? | May cause higher CPU usage? |
| -------- | --------------------- | ---------------------------------------- | -------------------------- |
| Simple | No | Yes | No |
| Parallel | Yes | No | Yes |

Essentially, choose the parallel loop executors if you're focusing on raw performance and accelerating workloads. To conserve battery usage, prevent higher CPU usage, or run in single-threaded environments, choose the simple loop executors.
