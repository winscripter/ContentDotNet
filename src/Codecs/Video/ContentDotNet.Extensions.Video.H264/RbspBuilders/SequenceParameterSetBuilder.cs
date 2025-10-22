namespace ContentDotNet.Extensions.Video.H264.RbspBuilders
{
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    internal class SequenceParameterSetBuilder
    {
        private uint profile, level, id, chromaFormat, picW, picH, bdL, bdC;
        private bool c0, c1, c2, c3, c4, c5, separateColourPlaneFlag, mbsOnlyFlag, mbaffFlag;
        private RbspVuiParameters? vuip;

        public SequenceParameterSetBuilder()
        {
        }

        public SequenceParameterSetBuilder ProfileLevel(uint profile, uint level)
        {
            this.profile = profile;
            this.level = level;
            return this;
        }

        public SequenceParameterSetBuilder Id(uint spsId)
        {
            this.id = spsId;
            return this;
        }

        public SequenceParameterSetBuilder Constraints(
            bool constraint0, bool constraint1, bool constraint2, bool constraint3, bool constraint4, bool constraint5)
        {
            c0 = constraint0;
            c1 = constraint1;
            c2 = constraint2;
            c3 = constraint3;
            c4 = constraint4;
            c5 = constraint5;
            return this;
        }

        public SequenceParameterSetBuilder ChromaFormat(
            uint chromaFormatIdc, bool separateColourPlaneFlag)
        {
            this.chromaFormat = chromaFormatIdc;
            this.separateColourPlaneFlag = separateColourPlaneFlag;
            return this;
        }

        public SequenceParameterSetBuilder PictureSize(
            uint picWidthInMbsMinus1, uint picHeightInMapUnitsMinus1)
        {
            picW = picWidthInMbsMinus1;
            picH = picHeightInMapUnitsMinus1;
            return this;
        }

        public SequenceParameterSetBuilder BitDepth(
            uint bitDepthLumaMinus8, uint bitDepthChromaMinus8)
        {
            this.bdL = bitDepthLumaMinus8;
            this.bdC = bitDepthChromaMinus8;
            return this;
        }

        public SequenceParameterSetBuilder FrameMbs(
            bool frameMbsOnlyFlag, bool mbaffFlag)
        {
            this.mbsOnlyFlag = frameMbsOnlyFlag;
            this.mbaffFlag = mbaffFlag;
            return this;
        }

        public SequenceParameterSetBuilder Vui(RbspVuiParameters? vuiParameters)
        {
            this.vuip = vuiParameters;
            return this;
        }

        public RbspSequenceParameterSetData Build()
        {
            return new(this.profile, this.c0, this.c1, this.c2, this.c3, this.c4, this.c5, 0, this.level, this.id, this.chromaFormat, this.separateColourPlaneFlag,
                this.bdL, this.bdC, false, false, [], new([], [], [], []), 0, 0, 0, false, 0, 0, 0, [], 0, false, this.picW, this.picH, this.mbsOnlyFlag, this.mbaffFlag,
                false, false, 0, 0, 0, 0, this.vuip != null, vuip);
        }
    }
}
