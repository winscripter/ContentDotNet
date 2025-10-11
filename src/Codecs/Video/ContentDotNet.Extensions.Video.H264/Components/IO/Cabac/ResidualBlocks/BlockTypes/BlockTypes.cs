namespace ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks.BlockTypes
{
    using ContentDotNet.Extensions.Video.H264.Components.IO.Cabac.ResidualBlocks.Dimensional;
    using ContentDotNet.Extensions.Video.H264.RbspModels;

    
    /// <summary>
    ///    Represents the Intra16x16DCLevel residual block type.
    /// </summary>
    internal class IntraDcResidualBlock : ResidualBlock1D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="IntraDcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public IntraDcResidualBlock(RbspResidual residual, List<int> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="IntraDcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public IntraDcResidualBlock(RbspResidual residual)
            : this(residual, residual.Intra16x16DCLevel ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    
    /// <summary>
    ///    Represents the CbIntra16x16DCLevel residual block type.
    /// </summary>
    internal class CbDcResidualBlock : ResidualBlock1D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="CbDcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public CbDcResidualBlock(RbspResidual residual, List<int> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="CbDcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public CbDcResidualBlock(RbspResidual residual)
            : this(residual, residual.CbIntra16x16DCLevel ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    
    /// <summary>
    ///    Represents the CrIntra16x16DCLevel residual block type.
    /// </summary>
    internal class CrDcResidualBlock : ResidualBlock1D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="CrDcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public CrDcResidualBlock(RbspResidual residual, List<int> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="CrDcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public CrDcResidualBlock(RbspResidual residual)
            : this(residual, residual.CrIntra16x16DCLevel ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    
    /// <summary>
    ///    Represents the ChromaDCLevel residual block type.
    /// </summary>
    internal class ChromaDcResidualBlock : ResidualBlock2D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="ChromaDcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public ChromaDcResidualBlock(RbspResidual residual, List<List<int>> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="ChromaDcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public ChromaDcResidualBlock(RbspResidual residual)
            : this(residual, residual.ChromaDCLevel ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    
    /// <summary>
    ///    Represents the Intra16x16ACLevel residual block type.
    /// </summary>
    internal class IntraAcResidualBlock : ResidualBlock2D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="IntraAcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public IntraAcResidualBlock(RbspResidual residual, List<List<int>> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="IntraAcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public IntraAcResidualBlock(RbspResidual residual)
            : this(residual, residual.Intra16x16ACLevel ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    
    /// <summary>
    ///    Represents the CbIntra16x16ACLevel residual block type.
    /// </summary>
    internal class CbAcResidualBlock : ResidualBlock2D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="CbAcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public CbAcResidualBlock(RbspResidual residual, List<List<int>> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="CbAcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public CbAcResidualBlock(RbspResidual residual)
            : this(residual, residual.CbIntra16x16ACLevel ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    
    /// <summary>
    ///    Represents the CrIntra16x16ACLevel residual block type.
    /// </summary>
    internal class CrAcResidualBlock : ResidualBlock2D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="CrAcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public CrAcResidualBlock(RbspResidual residual, List<List<int>> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="CrAcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public CrAcResidualBlock(RbspResidual residual)
            : this(residual, residual.CrIntra16x16ACLevel ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    
    /// <summary>
    ///    Represents the ChromaACLevel residual block type.
    /// </summary>
    internal class ChromaAcResidualBlock : ResidualBlock3D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="ChromaAcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public ChromaAcResidualBlock(RbspResidual residual, List<List<List<int>>> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="ChromaAcResidualBlock" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public ChromaAcResidualBlock(RbspResidual residual)
            : this(residual, residual.ChromaACLevel ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    
    /// <summary>
    ///    Represents the LumaLevel8x8 residual block type.
    /// </summary>
    internal class LumaLevel8x8 : ResidualBlock2D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="LumaLevel8x8" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public LumaLevel8x8(RbspResidual residual, List<List<int>> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="LumaLevel8x8" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public LumaLevel8x8(RbspResidual residual)
            : this(residual, residual.LumaLevel8x8 ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    
    /// <summary>
    ///    Represents the CbLevel8x8 residual block type.
    /// </summary>
    internal class CbLevel8x8 : ResidualBlock2D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="CbLevel8x8" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public CbLevel8x8(RbspResidual residual, List<List<int>> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="CbLevel8x8" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public CbLevel8x8(RbspResidual residual)
            : this(residual, residual.CbLevel8x8 ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    
    /// <summary>
    ///    Represents the CrLevel8x8 residual block type.
    /// </summary>
    internal class CrLevel8x8 : ResidualBlock2D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="CrLevel8x8" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public CrLevel8x8(RbspResidual residual, List<List<int>> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="CrLevel8x8" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public CrLevel8x8(RbspResidual residual)
            : this(residual, residual.CrLevel8x8 ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    
    /// <summary>
    ///    Represents the LumaLevel4x4 residual block type.
    /// </summary>
    internal class LumaLevel4x4 : ResidualBlock2D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="LumaLevel4x4" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public LumaLevel4x4(RbspResidual residual, List<List<int>> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="LumaLevel4x4" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public LumaLevel4x4(RbspResidual residual)
            : this(residual, residual.LumaLevel4x4 ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    
    /// <summary>
    ///    Represents the CbLevel4x4 residual block type.
    /// </summary>
    internal class CbLevel4x4 : ResidualBlock2D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="CbLevel4x4" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public CbLevel4x4(RbspResidual residual, List<List<int>> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="CbLevel4x4" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public CbLevel4x4(RbspResidual residual)
            : this(residual, residual.CbLevel4x4 ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    
    /// <summary>
    ///    Represents the CrLevel4x4 residual block type.
    /// </summary>
    internal class CrLevel4x4 : ResidualBlock2D
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="CrLevel4x4" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        /// <param name="coefficients">The coefficients for the residual block.</param>
        public CrLevel4x4(RbspResidual residual, List<List<int>> coefficients)
            : base(residual, coefficients)
        {
        }

        /// <summary>
        ///    Initializes a new instance of the <see cref="CrLevel4x4" /> class.
        /// </summary>
        /// <param name="residual">The RBSP residual data.</param>
        public CrLevel4x4(RbspResidual residual)
            : this(residual, residual.CrLevel4x4 ?? throw ResidualBlockThrowHelper.MissingResidualBlock())
        {
        }
    }

    }
