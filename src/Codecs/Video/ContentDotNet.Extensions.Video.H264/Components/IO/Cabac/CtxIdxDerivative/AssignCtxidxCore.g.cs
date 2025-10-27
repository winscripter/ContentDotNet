namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.Abstractions;
    using ContentDotNet.Extensions.Video.H264.Models;

    public static partial class AssignCtxIdx
    {
        private static int Core(IH264CabacDecoder cd, H264State h264, H264MacroblockInfo currMB, int binIdx, int ctxIdxOffset)
        {
            int incrementalCtxIdx = 0;
            if (ctxIdxOffset == 0 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 3, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 3 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 3, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 3 && binIdx == 1)
            {
                return 276;
            }
            else if (ctxIdxOffset == 3 && binIdx == 2)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 3 && binIdx == 3)
            {
                incrementalCtxIdx = 4;
            }
            else if (ctxIdxOffset == 3 && binIdx == 4)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 22, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 3 && binIdx == 5)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 22, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 3 && binIdx >= 6)
            {
                incrementalCtxIdx = 7;
            }
            else if (ctxIdxOffset == 11 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 1, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 14 && binIdx == 0)
            {
                incrementalCtxIdx = 0;
            }
            else if (ctxIdxOffset == 14 && binIdx == 1)
            {
                incrementalCtxIdx = 1;
            }
            else if (ctxIdxOffset == 14 && binIdx == 2)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 22, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 17 && binIdx == 0)
            {
                incrementalCtxIdx = 0;
            }
            else if (ctxIdxOffset == 17 && binIdx == 1)
            {
                return 276;
            }
            else if (ctxIdxOffset == 17 && binIdx == 2)
            {
                incrementalCtxIdx = 1;
            }
            else if (ctxIdxOffset == 17 && binIdx == 3)
            {
                incrementalCtxIdx = 2;
            }
            else if (ctxIdxOffset == 17 && binIdx == 4)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 22, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 17 && binIdx == 5)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 17 && binIdx >= 6)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 21 && binIdx == 0)
            {
                incrementalCtxIdx = 0;
            }
            else if (ctxIdxOffset == 21 && binIdx == 1)
            {
                incrementalCtxIdx = 1;
            }
            else if (ctxIdxOffset == 21 && binIdx == 2)
            {
                incrementalCtxIdx = 2;
            }
            else if (ctxIdxOffset == 24 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 1, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 27 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 3, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 27 && binIdx == 1)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 27 && binIdx == 2)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 22, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 27 && binIdx == 3)
            {
                incrementalCtxIdx = 5;
            }
            else if (ctxIdxOffset == 27 && binIdx == 4)
            {
                incrementalCtxIdx = 5;
            }
            else if (ctxIdxOffset == 27 && binIdx == 5)
            {
                incrementalCtxIdx = 5;
            }
            else if (ctxIdxOffset == 27 && binIdx >= 6)
            {
                incrementalCtxIdx = 5;
            }
            else if (ctxIdxOffset == 32 && binIdx == 0)
            {
                incrementalCtxIdx = 0;
            }
            else if (ctxIdxOffset == 32 && binIdx == 1)
            {
                return 276;
            }
            else if (ctxIdxOffset == 32 && binIdx == 2)
            {
                incrementalCtxIdx = 1;
            }
            else if (ctxIdxOffset == 32 && binIdx == 3)
            {
                incrementalCtxIdx = 2;
            }
            else if (ctxIdxOffset == 32 && binIdx == 4)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 22, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 32 && binIdx == 5)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 32 && binIdx >= 6)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 36 && binIdx == 0)
            {
                incrementalCtxIdx = 0;
            }
            else if (ctxIdxOffset == 36 && binIdx == 1)
            {
                incrementalCtxIdx = 1;
            }
            else if (ctxIdxOffset == 36 && binIdx == 2)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 22, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 36 && binIdx == 3)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 36 && binIdx == 4)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 36 && binIdx == 5)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 40 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 7, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 40 && binIdx == 1)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 40 && binIdx == 2)
            {
                incrementalCtxIdx = 4;
            }
            else if (ctxIdxOffset == 40 && binIdx == 3)
            {
                incrementalCtxIdx = 5;
            }
            else if (ctxIdxOffset == 40 && binIdx == 4)
            {
                incrementalCtxIdx = 6;
            }
            else if (ctxIdxOffset == 40 && binIdx == 5)
            {
                incrementalCtxIdx = 6;
            }
            else if (ctxIdxOffset == 40 && binIdx >= 6)
            {
                incrementalCtxIdx = 6;
            }
            else if (ctxIdxOffset == 47 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 7, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 47 && binIdx == 1)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 47 && binIdx == 2)
            {
                incrementalCtxIdx = 4;
            }
            else if (ctxIdxOffset == 47 && binIdx == 3)
            {
                incrementalCtxIdx = 5;
            }
            else if (ctxIdxOffset == 47 && binIdx == 4)
            {
                incrementalCtxIdx = 6;
            }
            else if (ctxIdxOffset == 47 && binIdx == 5)
            {
                incrementalCtxIdx = 6;
            }
            else if (ctxIdxOffset == 47 && binIdx >= 6)
            {
                incrementalCtxIdx = 6;
            }
            else if (ctxIdxOffset == 54 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 6, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 54 && binIdx == 1)
            {
                incrementalCtxIdx = 4;
            }
            else if (ctxIdxOffset == 54 && binIdx == 2)
            {
                incrementalCtxIdx = 5;
            }
            else if (ctxIdxOffset == 54 && binIdx == 3)
            {
                incrementalCtxIdx = 5;
            }
            else if (ctxIdxOffset == 54 && binIdx == 4)
            {
                incrementalCtxIdx = 5;
            }
            else if (ctxIdxOffset == 54 && binIdx == 5)
            {
                incrementalCtxIdx = 5;
            }
            else if (ctxIdxOffset == 54 && binIdx >= 6)
            {
                incrementalCtxIdx = 5;
            }
            else if (ctxIdxOffset == 60 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 5, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 60 && binIdx == 1)
            {
                incrementalCtxIdx = 2;
            }
            else if (ctxIdxOffset == 60 && binIdx == 2)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 60 && binIdx == 3)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 60 && binIdx == 4)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 60 && binIdx == 5)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 60 && binIdx >= 6)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 64 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 8, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 64 && binIdx == 1)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 64 && binIdx == 2)
            {
                incrementalCtxIdx = 3;
            }
            else if (ctxIdxOffset == 68 && binIdx == 0)
            {
                incrementalCtxIdx = 0;
            }
            else if (ctxIdxOffset == 69 && binIdx == 0)
            {
                incrementalCtxIdx = 0;
            }
            else if (ctxIdxOffset == 69 && binIdx == 1)
            {
                incrementalCtxIdx = 0;
            }
            else if (ctxIdxOffset == 69 && binIdx == 2)
            {
                incrementalCtxIdx = 0;
            }
            else if (ctxIdxOffset == 70 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 2, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 73 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 4, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 73 && binIdx == 1)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 4, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 73 && binIdx == 2)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 4, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 73 && binIdx == 3)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 4, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 77 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 4, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 77 && binIdx == 1)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 4, h264, currMB, binIdx, ctxIdxOffset);
            }
            else if (ctxIdxOffset == 276 && binIdx == 0)
            {
                incrementalCtxIdx = 0;
            }
            else if (ctxIdxOffset == 399 && binIdx == 0)
            {
                incrementalCtxIdx = InvokeCiiFunction(cd, 10, h264, currMB, binIdx, ctxIdxOffset);
            }
            return incrementalCtxIdx + ctxIdxOffset;
        }
    }
}
