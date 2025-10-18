// Yes we're generating tests now.

namespace ContentDotNet.Tests.H264.IO.Cabac
{
	using ContentDotNet.Tests.H264.TestTools.IO.Cabac;

	public partial class BinarizationReaderTests
	{
		[Fact]
		public void MbType_B_0()
		{
			var bits = new[]
			{
				false
			};

			Assert.Equal(0, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_1()
		{
			var bits = new[]
			{
				true, false, false
			};

			Assert.Equal(1, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_2()
		{
			var bits = new[]
			{
				true, false, true
			};

			Assert.Equal(2, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_3()
		{
			var bits = new[]
			{
				true, true, false, false, false, false
			};

			Assert.Equal(3, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_4()
		{
			var bits = new[]
			{
				true, true, false, false, false, true
			};

			Assert.Equal(4, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_5()
		{
			var bits = new[]
			{
				true, true, false, false, true, false
			};

			Assert.Equal(5, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_6()
		{
			var bits = new[]
			{
				true, true, false, false, true, true
			};

			Assert.Equal(6, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_7()
		{
			var bits = new[]
			{
				true, true, false, true, false, false
			};

			Assert.Equal(7, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_8()
		{
			var bits = new[]
			{
				true, true, false, true, false, true
			};

			Assert.Equal(8, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_9()
		{
			var bits = new[]
			{
				true, true, false, true, true, false
			};

			Assert.Equal(9, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_10()
		{
			var bits = new[]
			{
				true, true, false, true, true, true
			};

			Assert.Equal(10, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_11()
		{
			var bits = new[]
			{
				true, true, true, true, true, false
			};

			Assert.Equal(11, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_12()
		{
			var bits = new[]
			{
				true, true, true, false, false, false, false
			};

			Assert.Equal(12, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_13()
		{
			var bits = new[]
			{
				true, true, true, false, false, false, true
			};

			Assert.Equal(13, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_14()
		{
			var bits = new[]
			{
				true, true, true, false, false, true, false
			};

			Assert.Equal(14, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_15()
		{
			var bits = new[]
			{
				true, true, true, false, false, true, true
			};

			Assert.Equal(15, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_16()
		{
			var bits = new[]
			{
				true, true, true, false, true, false, false
			};

			Assert.Equal(16, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_17()
		{
			var bits = new[]
			{
				true, true, true, false, true, false, true
			};

			Assert.Equal(17, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_18()
		{
			var bits = new[]
			{
				true, true, true, false, true, true, false
			};

			Assert.Equal(18, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_19()
		{
			var bits = new[]
			{
				true, true, true, false, true, true, true
			};

			Assert.Equal(19, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_20()
		{
			var bits = new[]
			{
				true, true, true, true, false, false, false
			};

			Assert.Equal(20, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_21()
		{
			var bits = new[]
			{
				true, true, true, true, false, false, true
			};

			Assert.Equal(21, TestMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_B_22()
		{
			var bits = new[]
			{
				true, true, true, true, true, true
			};

			Assert.Equal(22, TestMbType_B(new BinCustomDecoder(bits)));
		}


		[Fact]
		public void MbType_PSP_0()
		{
			var bits = new[]
			{
				false, false, false
			};

			Assert.Equal(0, TestMbType_P(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_PSP_1()
		{
			var bits = new[]
			{
				false, true, true
			};

			Assert.Equal(1, TestMbType_P(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_PSP_2()
		{
			var bits = new[]
			{
				false, true, false
			};

			Assert.Equal(2, TestMbType_P(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_PSP_3()
		{
			var bits = new[]
			{
				false, false, true
			};

			Assert.Equal(3, TestMbType_P(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_PSP_5()
		{
			var bits = new[]
			{
				true
			};

			Assert.Equal(5, TestMbType_P(new BinCustomDecoder(bits)));
		}


		[Fact]
		public void MbType_I_0()
		{
			var bits = new[]
			{
				false
			};

			Assert.Equal(0, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_1()
		{
			var bits = new[]
			{
				true, false, false, false, false, false
			};

			Assert.Equal(1, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_2()
		{
			var bits = new[]
			{
				true, false, false, false, false, true
			};

			Assert.Equal(2, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_3()
		{
			var bits = new[]
			{
				true, false, false, false, true, false
			};

			Assert.Equal(3, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_4()
		{
			var bits = new[]
			{
				true, false, false, false, true, true
			};

			Assert.Equal(4, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_5()
		{
			var bits = new[]
			{
				true, false, false, true, false, false, false
			};

			Assert.Equal(5, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_6()
		{
			var bits = new[]
			{
				true, false, false, true, false, false, true
			};

			Assert.Equal(6, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_7()
		{
			var bits = new[]
			{
				true, false, false, true, false, true, false
			};

			Assert.Equal(7, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_8()
		{
			var bits = new[]
			{
				true, false, false, true, false, true, true
			};

			Assert.Equal(8, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_9()
		{
			var bits = new[]
			{
				true, false, false, true, true, false, false
			};

			Assert.Equal(9, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_10()
		{
			var bits = new[]
			{
				true, false, false, true, true, false, true
			};

			Assert.Equal(10, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_11()
		{
			var bits = new[]
			{
				true, false, false, true, true, true, false
			};

			Assert.Equal(11, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_12()
		{
			var bits = new[]
			{
				true, false, false, true, true, true, true
			};

			Assert.Equal(12, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_13()
		{
			var bits = new[]
			{
				true, false, true, false, false, false
			};

			Assert.Equal(13, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_14()
		{
			var bits = new[]
			{
				true, false, true, false, false, true
			};

			Assert.Equal(14, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_15()
		{
			var bits = new[]
			{
				true, false, true, false, true, false
			};

			Assert.Equal(15, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_16()
		{
			var bits = new[]
			{
				true, false, true, false, true, true
			};

			Assert.Equal(16, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_17()
		{
			var bits = new[]
			{
				true, false, true, true, false, false, false
			};

			Assert.Equal(17, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_18()
		{
			var bits = new[]
			{
				true, false, true, true, false, false, true
			};

			Assert.Equal(18, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_19()
		{
			var bits = new[]
			{
				true, false, true, true, false, true, false
			};

			Assert.Equal(19, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_20()
		{
			var bits = new[]
			{
				true, false, true, true, false, true, true
			};

			Assert.Equal(20, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_21()
		{
			var bits = new[]
			{
				true, false, true, true, true, false, false
			};

			Assert.Equal(21, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_22()
		{
			var bits = new[]
			{
				true, false, true, true, true, false, true
			};

			Assert.Equal(22, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_23()
		{
			var bits = new[]
			{
				true, false, true, true, true, true, false
			};

			Assert.Equal(23, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_24()
		{
			var bits = new[]
			{
				true, false, true, true, true, true, true
			};

			Assert.Equal(24, TestMbType_I(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void MbType_I_25()
		{
			var bits = new[]
			{
				true, true
			};

			Assert.Equal(25, TestMbType_I(new BinCustomDecoder(bits)));
		}


		[Fact]
		public void SubMbType_P_0()
		{
			var bits = new[]
			{
				true
			};

			Assert.Equal(0, TestSubMbType_P(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_P_1()
		{
			var bits = new[]
			{
				false, false
			};

			Assert.Equal(1, TestSubMbType_P(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_P_2()
		{
			var bits = new[]
			{
				false, true, true
			};

			Assert.Equal(2, TestSubMbType_P(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_P_3()
		{
			var bits = new[]
			{
				false, true, false
			};

			Assert.Equal(3, TestSubMbType_P(new BinCustomDecoder(bits)));
		}


		[Fact]
		public void SubMbType_B_0()
		{
			var bits = new[]
			{
				false
			};

			Assert.Equal(0, TestSubMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_B_1()
		{
			var bits = new[]
			{
				true, false, false
			};

			Assert.Equal(1, TestSubMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_B_2()
		{
			var bits = new[]
			{
				true, false, true
			};

			Assert.Equal(2, TestSubMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_B_3()
		{
			var bits = new[]
			{
				true, true, false, false, false
			};

			Assert.Equal(3, TestSubMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_B_4()
		{
			var bits = new[]
			{
				true, true, false, false, true
			};

			Assert.Equal(4, TestSubMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_B_5()
		{
			var bits = new[]
			{
				true, true, false, true, false
			};

			Assert.Equal(5, TestSubMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_B_6()
		{
			var bits = new[]
			{
				true, true, false, true, true
			};

			Assert.Equal(6, TestSubMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_B_7()
		{
			var bits = new[]
			{
				true, true, true, false, false, false
			};

			Assert.Equal(7, TestSubMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_B_8()
		{
			var bits = new[]
			{
				true, true, true, false, false, true
			};

			Assert.Equal(8, TestSubMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_B_9()
		{
			var bits = new[]
			{
				true, true, true, false, true, false
			};

			Assert.Equal(9, TestSubMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_B_10()
		{
			var bits = new[]
			{
				true, true, true, false, true, true
			};

			Assert.Equal(10, TestSubMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_B_11()
		{
			var bits = new[]
			{
				true, true, true, true, false
			};

			Assert.Equal(11, TestSubMbType_B(new BinCustomDecoder(bits)));
		}

		[Fact]
		public void SubMbType_B_12()
		{
			var bits = new[]
			{
				true, true, true, true, true
			};

			Assert.Equal(12, TestSubMbType_B(new BinCustomDecoder(bits)));
		}

	}
}
