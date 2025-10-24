#nullable enable

namespace ContentDotNet.Protocols.Rtsp.Headers
{
	using ContentDotNet.Protocols.Rtsp.Headers.Enumerations;
	using ContentDotNet.Protocols.Rtsp.Headers.Records;
	

	/// <summary>
	///   Represents the Accept RTSP header.
	/// </summary>
	public interface IRtspAcceptHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Value.
		/// </summary>
	    List<AcceptRecord> Value
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the AcceptCredentials RTSP header.
	/// </summary>
	public interface IRtspAcceptCredentialsHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Policy.
		/// </summary>
	    AcceptCredentialsPolicy Policy
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Value.
		/// </summary>
	    List<AcceptCredentialsRecord> Value
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the AcceptEncoding RTSP header.
	/// </summary>
	public interface IRtspAcceptEncodingHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Value.
		/// </summary>
	    List<AcceptEncodingRecord> Value
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the AcceptLanguage RTSP header.
	/// </summary>
	public interface IRtspAcceptLanguageHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Value.
		/// </summary>
	    List<AcceptLanguageRecord> Value
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the AcceptRanges RTSP header.
	/// </summary>
	public interface IRtspAcceptRangesHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Ranges.
		/// </summary>
	    List<string> Ranges
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Allow RTSP header.
	/// </summary>
	public interface IRtspAllowHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Methods.
		/// </summary>
	    List<string> Methods
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the AuthenticationInfo RTSP header.
	/// </summary>
	public interface IRtspAuthenticationInfoHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named NextNonce.
		/// </summary>
	    string? NextNonce
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named QualityOfProtection.
		/// </summary>
	    string? QualityOfProtection
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named ResponseAuthenticationDigest.
		/// </summary>
	    string? ResponseAuthenticationDigest
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named ClientNonce.
		/// </summary>
	    string? ClientNonce
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named NonceCount.
		/// </summary>
	    int? NonceCount
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Authorization RTSP header.
	/// </summary>
	public interface IRtspAuthorizationHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named DigestUsername.
		/// </summary>
	    string? DigestUsername
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Realm.
		/// </summary>
	    string? Realm
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Nonce.
		/// </summary>
	    string? Nonce
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Uri.
		/// </summary>
	    string? Uri
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Response.
		/// </summary>
	    string? Response
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named QualityOfProtection.
		/// </summary>
	    string? QualityOfProtection
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named NonceCount.
		/// </summary>
	    int? NonceCount
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named ClientNonce.
		/// </summary>
	    string? ClientNonce
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the BlockSize RTSP header.
	/// </summary>
	public interface IRtspBlockSizeHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Value.
		/// </summary>
	    int? Value
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the CacheControl RTSP header.
	/// </summary>
	public interface IRtspCacheControlHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named NoCache.
		/// </summary>
	    bool NoCache
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named NoStore.
		/// </summary>
	    bool NoStore
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named MustRevalidate.
		/// </summary>
	    bool MustRevalidate
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Public.
		/// </summary>
	    bool Public
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Private.
		/// </summary>
	    bool Private
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named MaxAge.
		/// </summary>
	    int? MaxAge
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Connection RTSP header.
	/// </summary>
	public interface IRtspConnectionHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Directives.
		/// </summary>
	    List<string> Directives
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the ContentBase RTSP header.
	/// </summary>
	public interface IRtspContentBaseHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Uri.
		/// </summary>
	    string? Uri
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the ContentEncoding RTSP header.
	/// </summary>
	public interface IRtspContentEncodingHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Encoding.
		/// </summary>
	    string? Encoding
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the ContentLanguage RTSP header.
	/// </summary>
	public interface IRtspContentLanguageHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Languages.
		/// </summary>
	    List<string> Languages
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the ContentLength RTSP header.
	/// </summary>
	public interface IRtspContentLengthHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Length.
		/// </summary>
	    int Length
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the ContentLocation RTSP header.
	/// </summary>
	public interface IRtspContentLocationHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Location.
		/// </summary>
	    string? Location
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the CSeq RTSP header.
	/// </summary>
	public interface IRtspCSeqHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named SequenceNumber.
		/// </summary>
	    int? SequenceNumber
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Date RTSP header.
	/// </summary>
	public interface IRtspDateHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Time.
		/// </summary>
	    string? Time
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Expires RTSP header.
	/// </summary>
	public interface IRtspExpiresHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Time.
		/// </summary>
	    string? Time
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the From RTSP header.
	/// </summary>
	public interface IRtspFromHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named EmailAddress.
		/// </summary>
	    string? EmailAddress
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the IfMatch RTSP header.
	/// </summary>
	public interface IRtspIfMatchHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named ETags.
		/// </summary>
	    List<string> ETags
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the IfModifiedSince RTSP header.
	/// </summary>
	public interface IRtspIfModifiedSinceHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Time.
		/// </summary>
	    string? Time
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the IfNoneMatch RTSP header.
	/// </summary>
	public interface IRtspIfNoneMatchHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named ETags.
		/// </summary>
	    List<string> ETags
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the LastModified RTSP header.
	/// </summary>
	public interface IRtspLastModifiedHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Time.
		/// </summary>
	    string? Time
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Location RTSP header.
	/// </summary>
	public interface IRtspLocationHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Uri.
		/// </summary>
	    string? Uri
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the MediaProperties RTSP header.
	/// </summary>
	public interface IRtspMediaPropertiesHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named RandomAccess.
		/// </summary>
	    double? RandomAccess
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named BeginningOnly.
		/// </summary>
	    bool BeginningOnly
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named NoSeeking.
		/// </summary>
	    bool NoSeeking
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Immutable.
		/// </summary>
	    bool Immutable
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Dynamic.
		/// </summary>
	    bool Dynamic
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named TimeProgressing.
		/// </summary>
	    bool TimeProgressing
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Unlimited.
		/// </summary>
	    bool Unlimited
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named TimeLimited.
		/// </summary>
	    bool TimeLimited
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named TimeDuration.
		/// </summary>
	    bool TimeDuration
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Scales.
		/// </summary>
	    string? Scales
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the MediaRange RTSP header.
	/// </summary>
	public interface IRtspMediaRangeHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named NormalPlayTime.
		/// </summary>
	    string? NormalPlayTime
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the MTag RTSP header.
	/// </summary>
	public interface IRtspMTagHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Value.
		/// </summary>
	    string? Value
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the NotifyReason RTSP header.
	/// </summary>
	public interface IRtspNotifyReasonHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named ReasonToken.
		/// </summary>
	    string? ReasonToken
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the PipelinedRequests RTSP header.
	/// </summary>
	public interface IRtspPipelinedRequestsHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Token.
		/// </summary>
	    string? Token
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the ProxyAuthenticate RTSP header.
	/// </summary>
	public interface IRtspProxyAuthenticateHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named AuthScheme.
		/// </summary>
	    string? AuthScheme
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Realm.
		/// </summary>
	    string? Realm
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Nonce.
		/// </summary>
	    string? Nonce
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Algorithm.
		/// </summary>
	    string? Algorithm
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the ProxyAuthenticationInfo RTSP header.
	/// </summary>
	public interface IRtspProxyAuthenticationInfoHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named NextNonce.
		/// </summary>
	    string? NextNonce
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named QualityOfProtection.
		/// </summary>
	    string? QualityOfProtection
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named RspAuth.
		/// </summary>
	    string? RspAuth
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the ProxyAuthorization RTSP header.
	/// </summary>
	public interface IRtspProxyAuthorizationHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named DigestUsername.
		/// </summary>
	    string? DigestUsername
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Realm.
		/// </summary>
	    string? Realm
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Nonce.
		/// </summary>
	    string? Nonce
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Uri.
		/// </summary>
	    string? Uri
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Response.
		/// </summary>
	    string? Response
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the ProxyRequire RTSP header.
	/// </summary>
	public interface IRtspProxyRequireHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Require.
		/// </summary>
	    string? Require
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the ProxySupported RTSP header.
	/// </summary>
	public interface IRtspProxySupportedHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Values.
		/// </summary>
	    List<string> Values
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Public RTSP header.
	/// </summary>
	public interface IRtspPublicHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Methods.
		/// </summary>
	    List<string> Methods
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Range RTSP header.
	/// </summary>
	public interface IRtspRangeHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Clock.
		/// </summary>
	    string? Clock
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named NormalPlayTime.
		/// </summary>
	    string? NormalPlayTime
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Referrer RTSP header.
	/// </summary>
	public interface IRtspReferrerHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Uri.
		/// </summary>
	    string? Uri
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the RequestStatus RTSP header.
	/// </summary>
	public interface IRtspRequestStatusHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named CSeq.
		/// </summary>
	    int? CSeq
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Status.
		/// </summary>
	    int? Status
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Reason.
		/// </summary>
	    string? Reason
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Require RTSP header.
	/// </summary>
	public interface IRtspRequireHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Feature.
		/// </summary>
	    string? Feature
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the RetryAfter RTSP header.
	/// </summary>
	public interface IRtspRetryAfterHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named DateOrTime.
		/// </summary>
	    string? DateOrTime
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the RtpInfo RTSP header.
	/// </summary>
	public interface IRtspRtpInfoHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Value.
		/// </summary>
	    List<RtpInfoRecord> Value
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Scale RTSP header.
	/// </summary>
	public interface IRtspScaleHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named ScaleValue.
		/// </summary>
	    double? ScaleValue
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the SeekStyle RTSP header.
	/// </summary>
	public interface IRtspSeekStyleHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Value.
		/// </summary>
	    string? Value
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Server RTSP header.
	/// </summary>
	public interface IRtspServerHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named ServerValue.
		/// </summary>
	    string? ServerValue
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Session RTSP header.
	/// </summary>
	public interface IRtspSessionHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named SessionId.
		/// </summary>
	    int? SessionId
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Timeout.
		/// </summary>
	    int? Timeout
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Speed RTSP header.
	/// </summary>
	public interface IRtspSpeedHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named SpeedRange.
		/// </summary>
	    string? SpeedRange
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Supported RTSP header.
	/// </summary>
	public interface IRtspSupportedHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named SupportedExtensions.
		/// </summary>
	    List<string> SupportedExtensions
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the TerminateReason RTSP header.
	/// </summary>
	public interface IRtspTerminateReasonHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Reason.
		/// </summary>
	    string? Reason
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Timestamp RTSP header.
	/// </summary>
	public interface IRtspTimestampHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named DateTime.
		/// </summary>
	    string? DateTime
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Transport RTSP header.
	/// </summary>
	public interface IRtspTransportHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named MediaDeliveryProtocol.
		/// </summary>
	    string? MediaDeliveryProtocol
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named TransportMethod.
		/// </summary>
	    string? TransportMethod
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Ssrc.
		/// </summary>
	    string? Ssrc
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named SourceAddress.
		/// </summary>
	    string? SourceAddress
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named DestinationAddress.
		/// </summary>
	    string? DestinationAddress
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named TimeToLive.
		/// </summary>
	    string? TimeToLive
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Mode.
		/// </summary>
	    string? Mode
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Interleaved.
		/// </summary>
	    bool Interleaved
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Unsupported RTSP header.
	/// </summary>
	public interface IRtspUnsupportedHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named UnsupportedExtensions.
		/// </summary>
	    List<string> UnsupportedExtensions
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the UserAgent RTSP header.
	/// </summary>
	public interface IRtspUserAgentHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Value.
		/// </summary>
	    string? Value
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the Via RTSP header.
	/// </summary>
	public interface IRtspViaHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named Value.
		/// </summary>
	    List<ViaRecord> Value
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Represents the WwwAuthenticate RTSP header.
	/// </summary>
	public interface IRtspWwwAuthenticateHeader : IRtspHeader
	{
				
		/// <summary>
		///   The value named DigestRealm.
		/// </summary>
	    string? DigestRealm
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Nonce.
		/// </summary>
	    string? Nonce
		{
			get;
			set;
		}

				
		/// <summary>
		///   The value named Algorithm.
		/// </summary>
	    string? Algorithm
		{
			get;
			set;
		}

			}


	/// <summary>
	///   Extensions for various RTSP headers.
	/// </summary>
	public static class RtspHeaderExtensions
	{
		
		/// <summary>
		///   Sets the <see cref="IRtspAcceptHeader.Value" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAcceptHeader WithValue(
			this IRtspAcceptHeader header,
			List<AcceptRecord> valueToReplaceWith)
		{
			header.Value = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAcceptCredentialsHeader.Policy" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAcceptCredentialsHeader WithPolicy(
			this IRtspAcceptCredentialsHeader header,
			AcceptCredentialsPolicy valueToReplaceWith)
		{
			header.Policy = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAcceptCredentialsHeader.Value" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAcceptCredentialsHeader WithValue(
			this IRtspAcceptCredentialsHeader header,
			List<AcceptCredentialsRecord> valueToReplaceWith)
		{
			header.Value = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAcceptEncodingHeader.Value" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAcceptEncodingHeader WithValue(
			this IRtspAcceptEncodingHeader header,
			List<AcceptEncodingRecord> valueToReplaceWith)
		{
			header.Value = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAcceptLanguageHeader.Value" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAcceptLanguageHeader WithValue(
			this IRtspAcceptLanguageHeader header,
			List<AcceptLanguageRecord> valueToReplaceWith)
		{
			header.Value = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAcceptRangesHeader.Ranges" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAcceptRangesHeader WithRanges(
			this IRtspAcceptRangesHeader header,
			List<string> valueToReplaceWith)
		{
			header.Ranges = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAllowHeader.Methods" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAllowHeader WithMethods(
			this IRtspAllowHeader header,
			List<string> valueToReplaceWith)
		{
			header.Methods = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAuthenticationInfoHeader.NextNonce" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAuthenticationInfoHeader WithNextNonce(
			this IRtspAuthenticationInfoHeader header,
			string? valueToReplaceWith)
		{
			header.NextNonce = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAuthenticationInfoHeader.QualityOfProtection" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAuthenticationInfoHeader WithQualityOfProtection(
			this IRtspAuthenticationInfoHeader header,
			string? valueToReplaceWith)
		{
			header.QualityOfProtection = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAuthenticationInfoHeader.ResponseAuthenticationDigest" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAuthenticationInfoHeader WithResponseAuthenticationDigest(
			this IRtspAuthenticationInfoHeader header,
			string? valueToReplaceWith)
		{
			header.ResponseAuthenticationDigest = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAuthenticationInfoHeader.ClientNonce" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAuthenticationInfoHeader WithClientNonce(
			this IRtspAuthenticationInfoHeader header,
			string? valueToReplaceWith)
		{
			header.ClientNonce = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAuthenticationInfoHeader.NonceCount" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAuthenticationInfoHeader WithNonceCount(
			this IRtspAuthenticationInfoHeader header,
			int? valueToReplaceWith)
		{
			header.NonceCount = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAuthorizationHeader.DigestUsername" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAuthorizationHeader WithDigestUsername(
			this IRtspAuthorizationHeader header,
			string? valueToReplaceWith)
		{
			header.DigestUsername = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAuthorizationHeader.Realm" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAuthorizationHeader WithRealm(
			this IRtspAuthorizationHeader header,
			string? valueToReplaceWith)
		{
			header.Realm = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAuthorizationHeader.Nonce" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAuthorizationHeader WithNonce(
			this IRtspAuthorizationHeader header,
			string? valueToReplaceWith)
		{
			header.Nonce = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAuthorizationHeader.Uri" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAuthorizationHeader WithUri(
			this IRtspAuthorizationHeader header,
			string? valueToReplaceWith)
		{
			header.Uri = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAuthorizationHeader.Response" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAuthorizationHeader WithResponse(
			this IRtspAuthorizationHeader header,
			string? valueToReplaceWith)
		{
			header.Response = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAuthorizationHeader.QualityOfProtection" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAuthorizationHeader WithQualityOfProtection(
			this IRtspAuthorizationHeader header,
			string? valueToReplaceWith)
		{
			header.QualityOfProtection = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAuthorizationHeader.NonceCount" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAuthorizationHeader WithNonceCount(
			this IRtspAuthorizationHeader header,
			int? valueToReplaceWith)
		{
			header.NonceCount = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspAuthorizationHeader.ClientNonce" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspAuthorizationHeader WithClientNonce(
			this IRtspAuthorizationHeader header,
			string? valueToReplaceWith)
		{
			header.ClientNonce = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspBlockSizeHeader.Value" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspBlockSizeHeader WithValue(
			this IRtspBlockSizeHeader header,
			int? valueToReplaceWith)
		{
			header.Value = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspCacheControlHeader.NoCache" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspCacheControlHeader WithNoCache(
			this IRtspCacheControlHeader header,
			bool valueToReplaceWith)
		{
			header.NoCache = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspCacheControlHeader.NoStore" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspCacheControlHeader WithNoStore(
			this IRtspCacheControlHeader header,
			bool valueToReplaceWith)
		{
			header.NoStore = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspCacheControlHeader.MustRevalidate" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspCacheControlHeader WithMustRevalidate(
			this IRtspCacheControlHeader header,
			bool valueToReplaceWith)
		{
			header.MustRevalidate = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspCacheControlHeader.Public" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspCacheControlHeader WithPublic(
			this IRtspCacheControlHeader header,
			bool valueToReplaceWith)
		{
			header.Public = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspCacheControlHeader.Private" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspCacheControlHeader WithPrivate(
			this IRtspCacheControlHeader header,
			bool valueToReplaceWith)
		{
			header.Private = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspCacheControlHeader.MaxAge" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspCacheControlHeader WithMaxAge(
			this IRtspCacheControlHeader header,
			int? valueToReplaceWith)
		{
			header.MaxAge = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspConnectionHeader.Directives" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspConnectionHeader WithDirectives(
			this IRtspConnectionHeader header,
			List<string> valueToReplaceWith)
		{
			header.Directives = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspContentBaseHeader.Uri" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspContentBaseHeader WithUri(
			this IRtspContentBaseHeader header,
			string? valueToReplaceWith)
		{
			header.Uri = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspContentEncodingHeader.Encoding" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspContentEncodingHeader WithEncoding(
			this IRtspContentEncodingHeader header,
			string? valueToReplaceWith)
		{
			header.Encoding = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspContentLanguageHeader.Languages" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspContentLanguageHeader WithLanguages(
			this IRtspContentLanguageHeader header,
			List<string> valueToReplaceWith)
		{
			header.Languages = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspContentLengthHeader.Length" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspContentLengthHeader WithLength(
			this IRtspContentLengthHeader header,
			int valueToReplaceWith)
		{
			header.Length = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspContentLocationHeader.Location" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspContentLocationHeader WithLocation(
			this IRtspContentLocationHeader header,
			string? valueToReplaceWith)
		{
			header.Location = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspCSeqHeader.SequenceNumber" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspCSeqHeader WithSequenceNumber(
			this IRtspCSeqHeader header,
			int? valueToReplaceWith)
		{
			header.SequenceNumber = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspDateHeader.Time" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspDateHeader WithTime(
			this IRtspDateHeader header,
			string? valueToReplaceWith)
		{
			header.Time = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspExpiresHeader.Time" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspExpiresHeader WithTime(
			this IRtspExpiresHeader header,
			string? valueToReplaceWith)
		{
			header.Time = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspFromHeader.EmailAddress" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspFromHeader WithEmailAddress(
			this IRtspFromHeader header,
			string? valueToReplaceWith)
		{
			header.EmailAddress = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspIfMatchHeader.ETags" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspIfMatchHeader WithETags(
			this IRtspIfMatchHeader header,
			List<string> valueToReplaceWith)
		{
			header.ETags = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspIfModifiedSinceHeader.Time" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspIfModifiedSinceHeader WithTime(
			this IRtspIfModifiedSinceHeader header,
			string? valueToReplaceWith)
		{
			header.Time = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspIfNoneMatchHeader.ETags" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspIfNoneMatchHeader WithETags(
			this IRtspIfNoneMatchHeader header,
			List<string> valueToReplaceWith)
		{
			header.ETags = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspLastModifiedHeader.Time" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspLastModifiedHeader WithTime(
			this IRtspLastModifiedHeader header,
			string? valueToReplaceWith)
		{
			header.Time = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspLocationHeader.Uri" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspLocationHeader WithUri(
			this IRtspLocationHeader header,
			string? valueToReplaceWith)
		{
			header.Uri = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspMediaPropertiesHeader.RandomAccess" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspMediaPropertiesHeader WithRandomAccess(
			this IRtspMediaPropertiesHeader header,
			double? valueToReplaceWith)
		{
			header.RandomAccess = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspMediaPropertiesHeader.BeginningOnly" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspMediaPropertiesHeader WithBeginningOnly(
			this IRtspMediaPropertiesHeader header,
			bool valueToReplaceWith)
		{
			header.BeginningOnly = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspMediaPropertiesHeader.NoSeeking" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspMediaPropertiesHeader WithNoSeeking(
			this IRtspMediaPropertiesHeader header,
			bool valueToReplaceWith)
		{
			header.NoSeeking = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspMediaPropertiesHeader.Immutable" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspMediaPropertiesHeader WithImmutable(
			this IRtspMediaPropertiesHeader header,
			bool valueToReplaceWith)
		{
			header.Immutable = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspMediaPropertiesHeader.Dynamic" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspMediaPropertiesHeader WithDynamic(
			this IRtspMediaPropertiesHeader header,
			bool valueToReplaceWith)
		{
			header.Dynamic = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspMediaPropertiesHeader.TimeProgressing" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspMediaPropertiesHeader WithTimeProgressing(
			this IRtspMediaPropertiesHeader header,
			bool valueToReplaceWith)
		{
			header.TimeProgressing = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspMediaPropertiesHeader.Unlimited" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspMediaPropertiesHeader WithUnlimited(
			this IRtspMediaPropertiesHeader header,
			bool valueToReplaceWith)
		{
			header.Unlimited = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspMediaPropertiesHeader.TimeLimited" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspMediaPropertiesHeader WithTimeLimited(
			this IRtspMediaPropertiesHeader header,
			bool valueToReplaceWith)
		{
			header.TimeLimited = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspMediaPropertiesHeader.TimeDuration" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspMediaPropertiesHeader WithTimeDuration(
			this IRtspMediaPropertiesHeader header,
			bool valueToReplaceWith)
		{
			header.TimeDuration = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspMediaPropertiesHeader.Scales" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspMediaPropertiesHeader WithScales(
			this IRtspMediaPropertiesHeader header,
			string? valueToReplaceWith)
		{
			header.Scales = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspMediaRangeHeader.NormalPlayTime" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspMediaRangeHeader WithNormalPlayTime(
			this IRtspMediaRangeHeader header,
			string? valueToReplaceWith)
		{
			header.NormalPlayTime = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspMTagHeader.Value" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspMTagHeader WithValue(
			this IRtspMTagHeader header,
			string? valueToReplaceWith)
		{
			header.Value = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspNotifyReasonHeader.ReasonToken" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspNotifyReasonHeader WithReasonToken(
			this IRtspNotifyReasonHeader header,
			string? valueToReplaceWith)
		{
			header.ReasonToken = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspPipelinedRequestsHeader.Token" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspPipelinedRequestsHeader WithToken(
			this IRtspPipelinedRequestsHeader header,
			string? valueToReplaceWith)
		{
			header.Token = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxyAuthenticateHeader.AuthScheme" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxyAuthenticateHeader WithAuthScheme(
			this IRtspProxyAuthenticateHeader header,
			string? valueToReplaceWith)
		{
			header.AuthScheme = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxyAuthenticateHeader.Realm" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxyAuthenticateHeader WithRealm(
			this IRtspProxyAuthenticateHeader header,
			string? valueToReplaceWith)
		{
			header.Realm = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxyAuthenticateHeader.Nonce" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxyAuthenticateHeader WithNonce(
			this IRtspProxyAuthenticateHeader header,
			string? valueToReplaceWith)
		{
			header.Nonce = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxyAuthenticateHeader.Algorithm" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxyAuthenticateHeader WithAlgorithm(
			this IRtspProxyAuthenticateHeader header,
			string? valueToReplaceWith)
		{
			header.Algorithm = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxyAuthenticationInfoHeader.NextNonce" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxyAuthenticationInfoHeader WithNextNonce(
			this IRtspProxyAuthenticationInfoHeader header,
			string? valueToReplaceWith)
		{
			header.NextNonce = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxyAuthenticationInfoHeader.QualityOfProtection" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxyAuthenticationInfoHeader WithQualityOfProtection(
			this IRtspProxyAuthenticationInfoHeader header,
			string? valueToReplaceWith)
		{
			header.QualityOfProtection = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxyAuthenticationInfoHeader.RspAuth" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxyAuthenticationInfoHeader WithRspAuth(
			this IRtspProxyAuthenticationInfoHeader header,
			string? valueToReplaceWith)
		{
			header.RspAuth = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxyAuthorizationHeader.DigestUsername" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxyAuthorizationHeader WithDigestUsername(
			this IRtspProxyAuthorizationHeader header,
			string? valueToReplaceWith)
		{
			header.DigestUsername = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxyAuthorizationHeader.Realm" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxyAuthorizationHeader WithRealm(
			this IRtspProxyAuthorizationHeader header,
			string? valueToReplaceWith)
		{
			header.Realm = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxyAuthorizationHeader.Nonce" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxyAuthorizationHeader WithNonce(
			this IRtspProxyAuthorizationHeader header,
			string? valueToReplaceWith)
		{
			header.Nonce = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxyAuthorizationHeader.Uri" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxyAuthorizationHeader WithUri(
			this IRtspProxyAuthorizationHeader header,
			string? valueToReplaceWith)
		{
			header.Uri = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxyAuthorizationHeader.Response" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxyAuthorizationHeader WithResponse(
			this IRtspProxyAuthorizationHeader header,
			string? valueToReplaceWith)
		{
			header.Response = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxyRequireHeader.Require" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxyRequireHeader WithRequire(
			this IRtspProxyRequireHeader header,
			string? valueToReplaceWith)
		{
			header.Require = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspProxySupportedHeader.Values" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspProxySupportedHeader WithValues(
			this IRtspProxySupportedHeader header,
			List<string> valueToReplaceWith)
		{
			header.Values = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspPublicHeader.Methods" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspPublicHeader WithMethods(
			this IRtspPublicHeader header,
			List<string> valueToReplaceWith)
		{
			header.Methods = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspRangeHeader.Clock" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspRangeHeader WithClock(
			this IRtspRangeHeader header,
			string? valueToReplaceWith)
		{
			header.Clock = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspRangeHeader.NormalPlayTime" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspRangeHeader WithNormalPlayTime(
			this IRtspRangeHeader header,
			string? valueToReplaceWith)
		{
			header.NormalPlayTime = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspReferrerHeader.Uri" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspReferrerHeader WithUri(
			this IRtspReferrerHeader header,
			string? valueToReplaceWith)
		{
			header.Uri = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspRequestStatusHeader.CSeq" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspRequestStatusHeader WithCSeq(
			this IRtspRequestStatusHeader header,
			int? valueToReplaceWith)
		{
			header.CSeq = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspRequestStatusHeader.Status" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspRequestStatusHeader WithStatus(
			this IRtspRequestStatusHeader header,
			int? valueToReplaceWith)
		{
			header.Status = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspRequestStatusHeader.Reason" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspRequestStatusHeader WithReason(
			this IRtspRequestStatusHeader header,
			string? valueToReplaceWith)
		{
			header.Reason = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspRequireHeader.Feature" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspRequireHeader WithFeature(
			this IRtspRequireHeader header,
			string? valueToReplaceWith)
		{
			header.Feature = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspRetryAfterHeader.DateOrTime" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspRetryAfterHeader WithDateOrTime(
			this IRtspRetryAfterHeader header,
			string? valueToReplaceWith)
		{
			header.DateOrTime = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspRtpInfoHeader.Value" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspRtpInfoHeader WithValue(
			this IRtspRtpInfoHeader header,
			List<RtpInfoRecord> valueToReplaceWith)
		{
			header.Value = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspScaleHeader.ScaleValue" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspScaleHeader WithScaleValue(
			this IRtspScaleHeader header,
			double? valueToReplaceWith)
		{
			header.ScaleValue = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspSeekStyleHeader.Value" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspSeekStyleHeader WithValue(
			this IRtspSeekStyleHeader header,
			string? valueToReplaceWith)
		{
			header.Value = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspServerHeader.ServerValue" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspServerHeader WithServerValue(
			this IRtspServerHeader header,
			string? valueToReplaceWith)
		{
			header.ServerValue = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspSessionHeader.SessionId" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspSessionHeader WithSessionId(
			this IRtspSessionHeader header,
			int? valueToReplaceWith)
		{
			header.SessionId = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspSessionHeader.Timeout" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspSessionHeader WithTimeout(
			this IRtspSessionHeader header,
			int? valueToReplaceWith)
		{
			header.Timeout = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspSpeedHeader.SpeedRange" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspSpeedHeader WithSpeedRange(
			this IRtspSpeedHeader header,
			string? valueToReplaceWith)
		{
			header.SpeedRange = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspSupportedHeader.SupportedExtensions" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspSupportedHeader WithSupportedExtensions(
			this IRtspSupportedHeader header,
			List<string> valueToReplaceWith)
		{
			header.SupportedExtensions = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspTerminateReasonHeader.Reason" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspTerminateReasonHeader WithReason(
			this IRtspTerminateReasonHeader header,
			string? valueToReplaceWith)
		{
			header.Reason = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspTimestampHeader.DateTime" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspTimestampHeader WithDateTime(
			this IRtspTimestampHeader header,
			string? valueToReplaceWith)
		{
			header.DateTime = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspTransportHeader.MediaDeliveryProtocol" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspTransportHeader WithMediaDeliveryProtocol(
			this IRtspTransportHeader header,
			string? valueToReplaceWith)
		{
			header.MediaDeliveryProtocol = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspTransportHeader.TransportMethod" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspTransportHeader WithTransportMethod(
			this IRtspTransportHeader header,
			string? valueToReplaceWith)
		{
			header.TransportMethod = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspTransportHeader.Ssrc" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspTransportHeader WithSsrc(
			this IRtspTransportHeader header,
			string? valueToReplaceWith)
		{
			header.Ssrc = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspTransportHeader.SourceAddress" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspTransportHeader WithSourceAddress(
			this IRtspTransportHeader header,
			string? valueToReplaceWith)
		{
			header.SourceAddress = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspTransportHeader.DestinationAddress" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspTransportHeader WithDestinationAddress(
			this IRtspTransportHeader header,
			string? valueToReplaceWith)
		{
			header.DestinationAddress = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspTransportHeader.TimeToLive" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspTransportHeader WithTimeToLive(
			this IRtspTransportHeader header,
			string? valueToReplaceWith)
		{
			header.TimeToLive = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspTransportHeader.Mode" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspTransportHeader WithMode(
			this IRtspTransportHeader header,
			string? valueToReplaceWith)
		{
			header.Mode = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspTransportHeader.Interleaved" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspTransportHeader WithInterleaved(
			this IRtspTransportHeader header,
			bool valueToReplaceWith)
		{
			header.Interleaved = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspUnsupportedHeader.UnsupportedExtensions" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspUnsupportedHeader WithUnsupportedExtensions(
			this IRtspUnsupportedHeader header,
			List<string> valueToReplaceWith)
		{
			header.UnsupportedExtensions = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspUserAgentHeader.Value" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspUserAgentHeader WithValue(
			this IRtspUserAgentHeader header,
			string? valueToReplaceWith)
		{
			header.Value = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspViaHeader.Value" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspViaHeader WithValue(
			this IRtspViaHeader header,
			List<ViaRecord> valueToReplaceWith)
		{
			header.Value = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspWwwAuthenticateHeader.DigestRealm" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspWwwAuthenticateHeader WithDigestRealm(
			this IRtspWwwAuthenticateHeader header,
			string? valueToReplaceWith)
		{
			header.DigestRealm = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspWwwAuthenticateHeader.Nonce" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspWwwAuthenticateHeader WithNonce(
			this IRtspWwwAuthenticateHeader header,
			string? valueToReplaceWith)
		{
			header.Nonce = valueToReplaceWith;
			return header;
		}

		
		/// <summary>
		///   Sets the <see cref="IRtspWwwAuthenticateHeader.Algorithm" /> property
		///   inside <paramref name="header" /> to be equal to <paramref name="valueToReplaceWith" />
		///   and returns the mutated <paramref name="header" />.
		/// </summary>
		/// <param name="header">The source RTSP header.</header>
		/// <param name="valueToReplaceWith">The value to mutate with.</param>
		/// <returns>
		///   The mutated <paramref name="header" />.
		/// </returns>
		public static IRtspWwwAuthenticateHeader WithAlgorithm(
			this IRtspWwwAuthenticateHeader header,
			string? valueToReplaceWith)
		{
			header.Algorithm = valueToReplaceWith;
			return header;
		}

	}

	/// <summary>
	///   Factory for creating RTSP headers.
	/// </summary>
	public interface IRtspHeaderFactory
	{
		/// <summary>
		///   Creates the blank RTSP header with the specified name.
		/// </summary>
		/// <param name="rtspHeaderName">RTSP header name</param>
		/// <returns>The RTSP header</returns>
		IRtspHeader Create(string rtspHeaderName);

		/// <summary>
		///   Returns the RTSP header of specified type.
		/// </summary>
		/// <typeparam name="T">The type of RTSP header to create</typeparam>
		/// <returns>The RTSP header</returns>
		T Create<T>() where T : IRtspHeader;

		/// <summary>
		///   Returns the RTSP header of specified type.
		/// </summary>
		/// <param name="type">The type of RTSP header to create</param>
		/// <returns>The RTSP header</returns>
		IRtspHeader Create(Type type);
	}

	/// <summary>
	///   Extensions for <see cref="IRtspHeaderFactory" />.
	/// </summary>
	public static class RtspHeaderFactoryExtensions
	{

		/// <summary>
		///   A shorthand to create IRtspAccept.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspAcceptHeader CreateEmptyAccept(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspAcceptHeader>();

		/// <summary>
		///   A shorthand to create IRtspAcceptCredentials.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspAcceptCredentialsHeader CreateEmptyAcceptCredentials(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspAcceptCredentialsHeader>();

		/// <summary>
		///   A shorthand to create IRtspAcceptEncoding.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspAcceptEncodingHeader CreateEmptyAcceptEncoding(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspAcceptEncodingHeader>();

		/// <summary>
		///   A shorthand to create IRtspAcceptLanguage.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspAcceptLanguageHeader CreateEmptyAcceptLanguage(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspAcceptLanguageHeader>();

		/// <summary>
		///   A shorthand to create IRtspAcceptRanges.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspAcceptRangesHeader CreateEmptyAcceptRanges(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspAcceptRangesHeader>();

		/// <summary>
		///   A shorthand to create IRtspAllow.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspAllowHeader CreateEmptyAllow(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspAllowHeader>();

		/// <summary>
		///   A shorthand to create IRtspAuthenticationInfo.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspAuthenticationInfoHeader CreateEmptyAuthenticationInfo(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspAuthenticationInfoHeader>();

		/// <summary>
		///   A shorthand to create IRtspAuthorization.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspAuthorizationHeader CreateEmptyAuthorization(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspAuthorizationHeader>();

		/// <summary>
		///   A shorthand to create IRtspBlockSize.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspBlockSizeHeader CreateEmptyBlockSize(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspBlockSizeHeader>();

		/// <summary>
		///   A shorthand to create IRtspCacheControl.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspCacheControlHeader CreateEmptyCacheControl(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspCacheControlHeader>();

		/// <summary>
		///   A shorthand to create IRtspConnection.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspConnectionHeader CreateEmptyConnection(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspConnectionHeader>();

		/// <summary>
		///   A shorthand to create IRtspContentBase.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspContentBaseHeader CreateEmptyContentBase(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspContentBaseHeader>();

		/// <summary>
		///   A shorthand to create IRtspContentEncoding.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspContentEncodingHeader CreateEmptyContentEncoding(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspContentEncodingHeader>();

		/// <summary>
		///   A shorthand to create IRtspContentLanguage.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspContentLanguageHeader CreateEmptyContentLanguage(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspContentLanguageHeader>();

		/// <summary>
		///   A shorthand to create IRtspContentLength.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspContentLengthHeader CreateEmptyContentLength(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspContentLengthHeader>();

		/// <summary>
		///   A shorthand to create IRtspContentLocation.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspContentLocationHeader CreateEmptyContentLocation(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspContentLocationHeader>();

		/// <summary>
		///   A shorthand to create IRtspCSeq.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspCSeqHeader CreateEmptyCSeq(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspCSeqHeader>();

		/// <summary>
		///   A shorthand to create IRtspDate.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspDateHeader CreateEmptyDate(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspDateHeader>();

		/// <summary>
		///   A shorthand to create IRtspExpires.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspExpiresHeader CreateEmptyExpires(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspExpiresHeader>();

		/// <summary>
		///   A shorthand to create IRtspFrom.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspFromHeader CreateEmptyFrom(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspFromHeader>();

		/// <summary>
		///   A shorthand to create IRtspIfMatch.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspIfMatchHeader CreateEmptyIfMatch(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspIfMatchHeader>();

		/// <summary>
		///   A shorthand to create IRtspIfModifiedSince.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspIfModifiedSinceHeader CreateEmptyIfModifiedSince(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspIfModifiedSinceHeader>();

		/// <summary>
		///   A shorthand to create IRtspIfNoneMatch.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspIfNoneMatchHeader CreateEmptyIfNoneMatch(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspIfNoneMatchHeader>();

		/// <summary>
		///   A shorthand to create IRtspLastModified.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspLastModifiedHeader CreateEmptyLastModified(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspLastModifiedHeader>();

		/// <summary>
		///   A shorthand to create IRtspLocation.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspLocationHeader CreateEmptyLocation(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspLocationHeader>();

		/// <summary>
		///   A shorthand to create IRtspMediaProperties.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspMediaPropertiesHeader CreateEmptyMediaProperties(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspMediaPropertiesHeader>();

		/// <summary>
		///   A shorthand to create IRtspMediaRange.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspMediaRangeHeader CreateEmptyMediaRange(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspMediaRangeHeader>();

		/// <summary>
		///   A shorthand to create IRtspMTag.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspMTagHeader CreateEmptyMTag(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspMTagHeader>();

		/// <summary>
		///   A shorthand to create IRtspNotifyReason.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspNotifyReasonHeader CreateEmptyNotifyReason(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspNotifyReasonHeader>();

		/// <summary>
		///   A shorthand to create IRtspPipelinedRequests.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspPipelinedRequestsHeader CreateEmptyPipelinedRequests(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspPipelinedRequestsHeader>();

		/// <summary>
		///   A shorthand to create IRtspProxyAuthenticate.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspProxyAuthenticateHeader CreateEmptyProxyAuthenticate(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspProxyAuthenticateHeader>();

		/// <summary>
		///   A shorthand to create IRtspProxyAuthenticationInfo.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspProxyAuthenticationInfoHeader CreateEmptyProxyAuthenticationInfo(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspProxyAuthenticationInfoHeader>();

		/// <summary>
		///   A shorthand to create IRtspProxyAuthorization.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspProxyAuthorizationHeader CreateEmptyProxyAuthorization(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspProxyAuthorizationHeader>();

		/// <summary>
		///   A shorthand to create IRtspProxyRequire.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspProxyRequireHeader CreateEmptyProxyRequire(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspProxyRequireHeader>();

		/// <summary>
		///   A shorthand to create IRtspProxySupported.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspProxySupportedHeader CreateEmptyProxySupported(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspProxySupportedHeader>();

		/// <summary>
		///   A shorthand to create IRtspPublic.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspPublicHeader CreateEmptyPublic(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspPublicHeader>();

		/// <summary>
		///   A shorthand to create IRtspRange.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspRangeHeader CreateEmptyRange(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspRangeHeader>();

		/// <summary>
		///   A shorthand to create IRtspReferrer.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspReferrerHeader CreateEmptyReferrer(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspReferrerHeader>();

		/// <summary>
		///   A shorthand to create IRtspRequestStatus.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspRequestStatusHeader CreateEmptyRequestStatus(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspRequestStatusHeader>();

		/// <summary>
		///   A shorthand to create IRtspRequire.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspRequireHeader CreateEmptyRequire(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspRequireHeader>();

		/// <summary>
		///   A shorthand to create IRtspRetryAfter.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspRetryAfterHeader CreateEmptyRetryAfter(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspRetryAfterHeader>();

		/// <summary>
		///   A shorthand to create IRtspRtpInfo.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspRtpInfoHeader CreateEmptyRtpInfo(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspRtpInfoHeader>();

		/// <summary>
		///   A shorthand to create IRtspScale.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspScaleHeader CreateEmptyScale(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspScaleHeader>();

		/// <summary>
		///   A shorthand to create IRtspSeekStyle.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspSeekStyleHeader CreateEmptySeekStyle(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspSeekStyleHeader>();

		/// <summary>
		///   A shorthand to create IRtspServer.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspServerHeader CreateEmptyServer(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspServerHeader>();

		/// <summary>
		///   A shorthand to create IRtspSession.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspSessionHeader CreateEmptySession(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspSessionHeader>();

		/// <summary>
		///   A shorthand to create IRtspSpeed.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspSpeedHeader CreateEmptySpeed(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspSpeedHeader>();

		/// <summary>
		///   A shorthand to create IRtspSupported.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspSupportedHeader CreateEmptySupported(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspSupportedHeader>();

		/// <summary>
		///   A shorthand to create IRtspTerminateReason.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspTerminateReasonHeader CreateEmptyTerminateReason(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspTerminateReasonHeader>();

		/// <summary>
		///   A shorthand to create IRtspTimestamp.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspTimestampHeader CreateEmptyTimestamp(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspTimestampHeader>();

		/// <summary>
		///   A shorthand to create IRtspTransport.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspTransportHeader CreateEmptyTransport(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspTransportHeader>();

		/// <summary>
		///   A shorthand to create IRtspUnsupported.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspUnsupportedHeader CreateEmptyUnsupported(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspUnsupportedHeader>();

		/// <summary>
		///   A shorthand to create IRtspUserAgent.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspUserAgentHeader CreateEmptyUserAgent(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspUserAgentHeader>();

		/// <summary>
		///   A shorthand to create IRtspVia.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspViaHeader CreateEmptyVia(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspViaHeader>();

		/// <summary>
		///   A shorthand to create IRtspWwwAuthenticate.
		/// </summary>
		/// <param name="factory">Input RTSP header factory</param>
		/// <returns>The created RTSP header</returns>
		public static IRtspWwwAuthenticateHeader CreateEmptyWwwAuthenticate(this IRtspHeaderFactory factory)
			=> factory.Create<IRtspWwwAuthenticateHeader>();

		/// <summary>
		///   Creates an <see cref="IRtspAcceptHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="value">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspAcceptHeader" /> with populated values.
		/// </returns>
		public static IRtspAcceptHeader CreateAccept(
			this IRtspHeaderFactory headerFactory,
			List<AcceptRecord> @value)
		{
			var theCreatedInterface = headerFactory.CreateEmptyAccept();
						theCreatedInterface.Value = @value;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspAcceptCredentialsHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="policy">The parameter.</param>
		/// <param name="value">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspAcceptCredentialsHeader" /> with populated values.
		/// </returns>
		public static IRtspAcceptCredentialsHeader CreateAcceptCredentials(
			this IRtspHeaderFactory headerFactory,
			AcceptCredentialsPolicy @policy, List<AcceptCredentialsRecord> @value)
		{
			var theCreatedInterface = headerFactory.CreateEmptyAcceptCredentials();
						theCreatedInterface.Policy = @policy;
			theCreatedInterface.Value = @value;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspAcceptEncodingHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="value">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspAcceptEncodingHeader" /> with populated values.
		/// </returns>
		public static IRtspAcceptEncodingHeader CreateAcceptEncoding(
			this IRtspHeaderFactory headerFactory,
			List<AcceptEncodingRecord> @value)
		{
			var theCreatedInterface = headerFactory.CreateEmptyAcceptEncoding();
						theCreatedInterface.Value = @value;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspAcceptLanguageHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="value">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspAcceptLanguageHeader" /> with populated values.
		/// </returns>
		public static IRtspAcceptLanguageHeader CreateAcceptLanguage(
			this IRtspHeaderFactory headerFactory,
			List<AcceptLanguageRecord> @value)
		{
			var theCreatedInterface = headerFactory.CreateEmptyAcceptLanguage();
						theCreatedInterface.Value = @value;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspAcceptRangesHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="ranges">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspAcceptRangesHeader" /> with populated values.
		/// </returns>
		public static IRtspAcceptRangesHeader CreateAcceptRanges(
			this IRtspHeaderFactory headerFactory,
			List<string> @ranges)
		{
			var theCreatedInterface = headerFactory.CreateEmptyAcceptRanges();
						theCreatedInterface.Ranges = @ranges;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspAllowHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="methods">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspAllowHeader" /> with populated values.
		/// </returns>
		public static IRtspAllowHeader CreateAllow(
			this IRtspHeaderFactory headerFactory,
			List<string> @methods)
		{
			var theCreatedInterface = headerFactory.CreateEmptyAllow();
						theCreatedInterface.Methods = @methods;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspAuthenticationInfoHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="nextNonce">The parameter.</param>
		/// <param name="qualityOfProtection">The parameter.</param>
		/// <param name="responseAuthenticationDigest">The parameter.</param>
		/// <param name="clientNonce">The parameter.</param>
		/// <param name="nonceCount">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspAuthenticationInfoHeader" /> with populated values.
		/// </returns>
		public static IRtspAuthenticationInfoHeader CreateAuthenticationInfo(
			this IRtspHeaderFactory headerFactory,
			string? @nextNonce, string? @qualityOfProtection, string? @responseAuthenticationDigest, string? @clientNonce, int? @nonceCount)
		{
			var theCreatedInterface = headerFactory.CreateEmptyAuthenticationInfo();
						theCreatedInterface.NextNonce = @nextNonce;
			theCreatedInterface.QualityOfProtection = @qualityOfProtection;
			theCreatedInterface.ResponseAuthenticationDigest = @responseAuthenticationDigest;
			theCreatedInterface.ClientNonce = @clientNonce;
			theCreatedInterface.NonceCount = @nonceCount;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspAuthorizationHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="digestUsername">The parameter.</param>
		/// <param name="realm">The parameter.</param>
		/// <param name="nonce">The parameter.</param>
		/// <param name="uri">The parameter.</param>
		/// <param name="response">The parameter.</param>
		/// <param name="qualityOfProtection">The parameter.</param>
		/// <param name="nonceCount">The parameter.</param>
		/// <param name="clientNonce">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspAuthorizationHeader" /> with populated values.
		/// </returns>
		public static IRtspAuthorizationHeader CreateAuthorization(
			this IRtspHeaderFactory headerFactory,
			string? @digestUsername, string? @realm, string? @nonce, string? @uri, string? @response, string? @qualityOfProtection, int? @nonceCount, string? @clientNonce)
		{
			var theCreatedInterface = headerFactory.CreateEmptyAuthorization();
						theCreatedInterface.DigestUsername = @digestUsername;
			theCreatedInterface.Realm = @realm;
			theCreatedInterface.Nonce = @nonce;
			theCreatedInterface.Uri = @uri;
			theCreatedInterface.Response = @response;
			theCreatedInterface.QualityOfProtection = @qualityOfProtection;
			theCreatedInterface.NonceCount = @nonceCount;
			theCreatedInterface.ClientNonce = @clientNonce;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspBlockSizeHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="value">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspBlockSizeHeader" /> with populated values.
		/// </returns>
		public static IRtspBlockSizeHeader CreateBlockSize(
			this IRtspHeaderFactory headerFactory,
			int? @value)
		{
			var theCreatedInterface = headerFactory.CreateEmptyBlockSize();
						theCreatedInterface.Value = @value;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspCacheControlHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="noCache">The parameter.</param>
		/// <param name="noStore">The parameter.</param>
		/// <param name="mustRevalidate">The parameter.</param>
		/// <param name="public">The parameter.</param>
		/// <param name="private">The parameter.</param>
		/// <param name="maxAge">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspCacheControlHeader" /> with populated values.
		/// </returns>
		public static IRtspCacheControlHeader CreateCacheControl(
			this IRtspHeaderFactory headerFactory,
			bool @noCache, bool @noStore, bool @mustRevalidate, bool @public, bool @private, int? @maxAge)
		{
			var theCreatedInterface = headerFactory.CreateEmptyCacheControl();
						theCreatedInterface.NoCache = @noCache;
			theCreatedInterface.NoStore = @noStore;
			theCreatedInterface.MustRevalidate = @mustRevalidate;
			theCreatedInterface.Public = @public;
			theCreatedInterface.Private = @private;
			theCreatedInterface.MaxAge = @maxAge;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspConnectionHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="directives">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspConnectionHeader" /> with populated values.
		/// </returns>
		public static IRtspConnectionHeader CreateConnection(
			this IRtspHeaderFactory headerFactory,
			List<string> @directives)
		{
			var theCreatedInterface = headerFactory.CreateEmptyConnection();
						theCreatedInterface.Directives = @directives;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspContentBaseHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="uri">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspContentBaseHeader" /> with populated values.
		/// </returns>
		public static IRtspContentBaseHeader CreateContentBase(
			this IRtspHeaderFactory headerFactory,
			string? @uri)
		{
			var theCreatedInterface = headerFactory.CreateEmptyContentBase();
						theCreatedInterface.Uri = @uri;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspContentEncodingHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="encoding">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspContentEncodingHeader" /> with populated values.
		/// </returns>
		public static IRtspContentEncodingHeader CreateContentEncoding(
			this IRtspHeaderFactory headerFactory,
			string? @encoding)
		{
			var theCreatedInterface = headerFactory.CreateEmptyContentEncoding();
						theCreatedInterface.Encoding = @encoding;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspContentLanguageHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="languages">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspContentLanguageHeader" /> with populated values.
		/// </returns>
		public static IRtspContentLanguageHeader CreateContentLanguage(
			this IRtspHeaderFactory headerFactory,
			List<string> @languages)
		{
			var theCreatedInterface = headerFactory.CreateEmptyContentLanguage();
						theCreatedInterface.Languages = @languages;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspContentLengthHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="length">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspContentLengthHeader" /> with populated values.
		/// </returns>
		public static IRtspContentLengthHeader CreateContentLength(
			this IRtspHeaderFactory headerFactory,
			int @length)
		{
			var theCreatedInterface = headerFactory.CreateEmptyContentLength();
						theCreatedInterface.Length = @length;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspContentLocationHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="location">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspContentLocationHeader" /> with populated values.
		/// </returns>
		public static IRtspContentLocationHeader CreateContentLocation(
			this IRtspHeaderFactory headerFactory,
			string? @location)
		{
			var theCreatedInterface = headerFactory.CreateEmptyContentLocation();
						theCreatedInterface.Location = @location;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspCSeqHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="sequenceNumber">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspCSeqHeader" /> with populated values.
		/// </returns>
		public static IRtspCSeqHeader CreateCSeq(
			this IRtspHeaderFactory headerFactory,
			int? @sequenceNumber)
		{
			var theCreatedInterface = headerFactory.CreateEmptyCSeq();
						theCreatedInterface.SequenceNumber = @sequenceNumber;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspDateHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="time">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspDateHeader" /> with populated values.
		/// </returns>
		public static IRtspDateHeader CreateDate(
			this IRtspHeaderFactory headerFactory,
			string? @time)
		{
			var theCreatedInterface = headerFactory.CreateEmptyDate();
						theCreatedInterface.Time = @time;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspExpiresHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="time">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspExpiresHeader" /> with populated values.
		/// </returns>
		public static IRtspExpiresHeader CreateExpires(
			this IRtspHeaderFactory headerFactory,
			string? @time)
		{
			var theCreatedInterface = headerFactory.CreateEmptyExpires();
						theCreatedInterface.Time = @time;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspFromHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="emailAddress">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspFromHeader" /> with populated values.
		/// </returns>
		public static IRtspFromHeader CreateFrom(
			this IRtspHeaderFactory headerFactory,
			string? @emailAddress)
		{
			var theCreatedInterface = headerFactory.CreateEmptyFrom();
						theCreatedInterface.EmailAddress = @emailAddress;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspIfMatchHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="eTags">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspIfMatchHeader" /> with populated values.
		/// </returns>
		public static IRtspIfMatchHeader CreateIfMatch(
			this IRtspHeaderFactory headerFactory,
			List<string> @eTags)
		{
			var theCreatedInterface = headerFactory.CreateEmptyIfMatch();
						theCreatedInterface.ETags = @eTags;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspIfModifiedSinceHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="time">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspIfModifiedSinceHeader" /> with populated values.
		/// </returns>
		public static IRtspIfModifiedSinceHeader CreateIfModifiedSince(
			this IRtspHeaderFactory headerFactory,
			string? @time)
		{
			var theCreatedInterface = headerFactory.CreateEmptyIfModifiedSince();
						theCreatedInterface.Time = @time;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspIfNoneMatchHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="eTags">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspIfNoneMatchHeader" /> with populated values.
		/// </returns>
		public static IRtspIfNoneMatchHeader CreateIfNoneMatch(
			this IRtspHeaderFactory headerFactory,
			List<string> @eTags)
		{
			var theCreatedInterface = headerFactory.CreateEmptyIfNoneMatch();
						theCreatedInterface.ETags = @eTags;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspLastModifiedHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="time">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspLastModifiedHeader" /> with populated values.
		/// </returns>
		public static IRtspLastModifiedHeader CreateLastModified(
			this IRtspHeaderFactory headerFactory,
			string? @time)
		{
			var theCreatedInterface = headerFactory.CreateEmptyLastModified();
						theCreatedInterface.Time = @time;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspLocationHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="uri">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspLocationHeader" /> with populated values.
		/// </returns>
		public static IRtspLocationHeader CreateLocation(
			this IRtspHeaderFactory headerFactory,
			string? @uri)
		{
			var theCreatedInterface = headerFactory.CreateEmptyLocation();
						theCreatedInterface.Uri = @uri;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspMediaPropertiesHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="randomAccess">The parameter.</param>
		/// <param name="beginningOnly">The parameter.</param>
		/// <param name="noSeeking">The parameter.</param>
		/// <param name="immutable">The parameter.</param>
		/// <param name="dynamic">The parameter.</param>
		/// <param name="timeProgressing">The parameter.</param>
		/// <param name="unlimited">The parameter.</param>
		/// <param name="timeLimited">The parameter.</param>
		/// <param name="timeDuration">The parameter.</param>
		/// <param name="scales">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspMediaPropertiesHeader" /> with populated values.
		/// </returns>
		public static IRtspMediaPropertiesHeader CreateMediaProperties(
			this IRtspHeaderFactory headerFactory,
			double? @randomAccess, bool @beginningOnly, bool @noSeeking, bool @immutable, bool @dynamic, bool @timeProgressing, bool @unlimited, bool @timeLimited, bool @timeDuration, string? @scales)
		{
			var theCreatedInterface = headerFactory.CreateEmptyMediaProperties();
						theCreatedInterface.RandomAccess = @randomAccess;
			theCreatedInterface.BeginningOnly = @beginningOnly;
			theCreatedInterface.NoSeeking = @noSeeking;
			theCreatedInterface.Immutable = @immutable;
			theCreatedInterface.Dynamic = @dynamic;
			theCreatedInterface.TimeProgressing = @timeProgressing;
			theCreatedInterface.Unlimited = @unlimited;
			theCreatedInterface.TimeLimited = @timeLimited;
			theCreatedInterface.TimeDuration = @timeDuration;
			theCreatedInterface.Scales = @scales;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspMediaRangeHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="normalPlayTime">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspMediaRangeHeader" /> with populated values.
		/// </returns>
		public static IRtspMediaRangeHeader CreateMediaRange(
			this IRtspHeaderFactory headerFactory,
			string? @normalPlayTime)
		{
			var theCreatedInterface = headerFactory.CreateEmptyMediaRange();
						theCreatedInterface.NormalPlayTime = @normalPlayTime;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspMTagHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="value">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspMTagHeader" /> with populated values.
		/// </returns>
		public static IRtspMTagHeader CreateMTag(
			this IRtspHeaderFactory headerFactory,
			string? @value)
		{
			var theCreatedInterface = headerFactory.CreateEmptyMTag();
						theCreatedInterface.Value = @value;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspNotifyReasonHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="reasonToken">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspNotifyReasonHeader" /> with populated values.
		/// </returns>
		public static IRtspNotifyReasonHeader CreateNotifyReason(
			this IRtspHeaderFactory headerFactory,
			string? @reasonToken)
		{
			var theCreatedInterface = headerFactory.CreateEmptyNotifyReason();
						theCreatedInterface.ReasonToken = @reasonToken;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspPipelinedRequestsHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="token">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspPipelinedRequestsHeader" /> with populated values.
		/// </returns>
		public static IRtspPipelinedRequestsHeader CreatePipelinedRequests(
			this IRtspHeaderFactory headerFactory,
			string? @token)
		{
			var theCreatedInterface = headerFactory.CreateEmptyPipelinedRequests();
						theCreatedInterface.Token = @token;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspProxyAuthenticateHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="authScheme">The parameter.</param>
		/// <param name="realm">The parameter.</param>
		/// <param name="nonce">The parameter.</param>
		/// <param name="algorithm">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspProxyAuthenticateHeader" /> with populated values.
		/// </returns>
		public static IRtspProxyAuthenticateHeader CreateProxyAuthenticate(
			this IRtspHeaderFactory headerFactory,
			string? @authScheme, string? @realm, string? @nonce, string? @algorithm)
		{
			var theCreatedInterface = headerFactory.CreateEmptyProxyAuthenticate();
						theCreatedInterface.AuthScheme = @authScheme;
			theCreatedInterface.Realm = @realm;
			theCreatedInterface.Nonce = @nonce;
			theCreatedInterface.Algorithm = @algorithm;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspProxyAuthenticationInfoHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="nextNonce">The parameter.</param>
		/// <param name="qualityOfProtection">The parameter.</param>
		/// <param name="rspAuth">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspProxyAuthenticationInfoHeader" /> with populated values.
		/// </returns>
		public static IRtspProxyAuthenticationInfoHeader CreateProxyAuthenticationInfo(
			this IRtspHeaderFactory headerFactory,
			string? @nextNonce, string? @qualityOfProtection, string? @rspAuth)
		{
			var theCreatedInterface = headerFactory.CreateEmptyProxyAuthenticationInfo();
						theCreatedInterface.NextNonce = @nextNonce;
			theCreatedInterface.QualityOfProtection = @qualityOfProtection;
			theCreatedInterface.RspAuth = @rspAuth;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspProxyAuthorizationHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="digestUsername">The parameter.</param>
		/// <param name="realm">The parameter.</param>
		/// <param name="nonce">The parameter.</param>
		/// <param name="uri">The parameter.</param>
		/// <param name="response">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspProxyAuthorizationHeader" /> with populated values.
		/// </returns>
		public static IRtspProxyAuthorizationHeader CreateProxyAuthorization(
			this IRtspHeaderFactory headerFactory,
			string? @digestUsername, string? @realm, string? @nonce, string? @uri, string? @response)
		{
			var theCreatedInterface = headerFactory.CreateEmptyProxyAuthorization();
						theCreatedInterface.DigestUsername = @digestUsername;
			theCreatedInterface.Realm = @realm;
			theCreatedInterface.Nonce = @nonce;
			theCreatedInterface.Uri = @uri;
			theCreatedInterface.Response = @response;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspProxyRequireHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="require">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspProxyRequireHeader" /> with populated values.
		/// </returns>
		public static IRtspProxyRequireHeader CreateProxyRequire(
			this IRtspHeaderFactory headerFactory,
			string? @require)
		{
			var theCreatedInterface = headerFactory.CreateEmptyProxyRequire();
						theCreatedInterface.Require = @require;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspProxySupportedHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="values">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspProxySupportedHeader" /> with populated values.
		/// </returns>
		public static IRtspProxySupportedHeader CreateProxySupported(
			this IRtspHeaderFactory headerFactory,
			List<string> @values)
		{
			var theCreatedInterface = headerFactory.CreateEmptyProxySupported();
						theCreatedInterface.Values = @values;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspPublicHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="methods">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspPublicHeader" /> with populated values.
		/// </returns>
		public static IRtspPublicHeader CreatePublic(
			this IRtspHeaderFactory headerFactory,
			List<string> @methods)
		{
			var theCreatedInterface = headerFactory.CreateEmptyPublic();
						theCreatedInterface.Methods = @methods;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspRangeHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="clock">The parameter.</param>
		/// <param name="normalPlayTime">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspRangeHeader" /> with populated values.
		/// </returns>
		public static IRtspRangeHeader CreateRange(
			this IRtspHeaderFactory headerFactory,
			string? @clock, string? @normalPlayTime)
		{
			var theCreatedInterface = headerFactory.CreateEmptyRange();
						theCreatedInterface.Clock = @clock;
			theCreatedInterface.NormalPlayTime = @normalPlayTime;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspReferrerHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="uri">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspReferrerHeader" /> with populated values.
		/// </returns>
		public static IRtspReferrerHeader CreateReferrer(
			this IRtspHeaderFactory headerFactory,
			string? @uri)
		{
			var theCreatedInterface = headerFactory.CreateEmptyReferrer();
						theCreatedInterface.Uri = @uri;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspRequestStatusHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="cSeq">The parameter.</param>
		/// <param name="status">The parameter.</param>
		/// <param name="reason">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspRequestStatusHeader" /> with populated values.
		/// </returns>
		public static IRtspRequestStatusHeader CreateRequestStatus(
			this IRtspHeaderFactory headerFactory,
			int? @cSeq, int? @status, string? @reason)
		{
			var theCreatedInterface = headerFactory.CreateEmptyRequestStatus();
						theCreatedInterface.CSeq = @cSeq;
			theCreatedInterface.Status = @status;
			theCreatedInterface.Reason = @reason;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspRequireHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="feature">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspRequireHeader" /> with populated values.
		/// </returns>
		public static IRtspRequireHeader CreateRequire(
			this IRtspHeaderFactory headerFactory,
			string? @feature)
		{
			var theCreatedInterface = headerFactory.CreateEmptyRequire();
						theCreatedInterface.Feature = @feature;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspRetryAfterHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="dateOrTime">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspRetryAfterHeader" /> with populated values.
		/// </returns>
		public static IRtspRetryAfterHeader CreateRetryAfter(
			this IRtspHeaderFactory headerFactory,
			string? @dateOrTime)
		{
			var theCreatedInterface = headerFactory.CreateEmptyRetryAfter();
						theCreatedInterface.DateOrTime = @dateOrTime;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspRtpInfoHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="value">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspRtpInfoHeader" /> with populated values.
		/// </returns>
		public static IRtspRtpInfoHeader CreateRtpInfo(
			this IRtspHeaderFactory headerFactory,
			List<RtpInfoRecord> @value)
		{
			var theCreatedInterface = headerFactory.CreateEmptyRtpInfo();
						theCreatedInterface.Value = @value;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspScaleHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="scaleValue">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspScaleHeader" /> with populated values.
		/// </returns>
		public static IRtspScaleHeader CreateScale(
			this IRtspHeaderFactory headerFactory,
			double? @scaleValue)
		{
			var theCreatedInterface = headerFactory.CreateEmptyScale();
						theCreatedInterface.ScaleValue = @scaleValue;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspSeekStyleHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="value">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspSeekStyleHeader" /> with populated values.
		/// </returns>
		public static IRtspSeekStyleHeader CreateSeekStyle(
			this IRtspHeaderFactory headerFactory,
			string? @value)
		{
			var theCreatedInterface = headerFactory.CreateEmptySeekStyle();
						theCreatedInterface.Value = @value;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspServerHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="serverValue">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspServerHeader" /> with populated values.
		/// </returns>
		public static IRtspServerHeader CreateServer(
			this IRtspHeaderFactory headerFactory,
			string? @serverValue)
		{
			var theCreatedInterface = headerFactory.CreateEmptyServer();
						theCreatedInterface.ServerValue = @serverValue;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspSessionHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="sessionId">The parameter.</param>
		/// <param name="timeout">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspSessionHeader" /> with populated values.
		/// </returns>
		public static IRtspSessionHeader CreateSession(
			this IRtspHeaderFactory headerFactory,
			int? @sessionId, int? @timeout)
		{
			var theCreatedInterface = headerFactory.CreateEmptySession();
						theCreatedInterface.SessionId = @sessionId;
			theCreatedInterface.Timeout = @timeout;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspSpeedHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="speedRange">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspSpeedHeader" /> with populated values.
		/// </returns>
		public static IRtspSpeedHeader CreateSpeed(
			this IRtspHeaderFactory headerFactory,
			string? @speedRange)
		{
			var theCreatedInterface = headerFactory.CreateEmptySpeed();
						theCreatedInterface.SpeedRange = @speedRange;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspSupportedHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="supportedExtensions">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspSupportedHeader" /> with populated values.
		/// </returns>
		public static IRtspSupportedHeader CreateSupported(
			this IRtspHeaderFactory headerFactory,
			List<string> @supportedExtensions)
		{
			var theCreatedInterface = headerFactory.CreateEmptySupported();
						theCreatedInterface.SupportedExtensions = @supportedExtensions;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspTerminateReasonHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="reason">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspTerminateReasonHeader" /> with populated values.
		/// </returns>
		public static IRtspTerminateReasonHeader CreateTerminateReason(
			this IRtspHeaderFactory headerFactory,
			string? @reason)
		{
			var theCreatedInterface = headerFactory.CreateEmptyTerminateReason();
						theCreatedInterface.Reason = @reason;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspTimestampHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="dateTime">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspTimestampHeader" /> with populated values.
		/// </returns>
		public static IRtspTimestampHeader CreateTimestamp(
			this IRtspHeaderFactory headerFactory,
			string? @dateTime)
		{
			var theCreatedInterface = headerFactory.CreateEmptyTimestamp();
						theCreatedInterface.DateTime = @dateTime;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspTransportHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="mediaDeliveryProtocol">The parameter.</param>
		/// <param name="transportMethod">The parameter.</param>
		/// <param name="ssrc">The parameter.</param>
		/// <param name="sourceAddress">The parameter.</param>
		/// <param name="destinationAddress">The parameter.</param>
		/// <param name="timeToLive">The parameter.</param>
		/// <param name="mode">The parameter.</param>
		/// <param name="interleaved">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspTransportHeader" /> with populated values.
		/// </returns>
		public static IRtspTransportHeader CreateTransport(
			this IRtspHeaderFactory headerFactory,
			string? @mediaDeliveryProtocol, string? @transportMethod, string? @ssrc, string? @sourceAddress, string? @destinationAddress, string? @timeToLive, string? @mode, bool @interleaved)
		{
			var theCreatedInterface = headerFactory.CreateEmptyTransport();
						theCreatedInterface.MediaDeliveryProtocol = @mediaDeliveryProtocol;
			theCreatedInterface.TransportMethod = @transportMethod;
			theCreatedInterface.Ssrc = @ssrc;
			theCreatedInterface.SourceAddress = @sourceAddress;
			theCreatedInterface.DestinationAddress = @destinationAddress;
			theCreatedInterface.TimeToLive = @timeToLive;
			theCreatedInterface.Mode = @mode;
			theCreatedInterface.Interleaved = @interleaved;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspUnsupportedHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="unsupportedExtensions">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspUnsupportedHeader" /> with populated values.
		/// </returns>
		public static IRtspUnsupportedHeader CreateUnsupported(
			this IRtspHeaderFactory headerFactory,
			List<string> @unsupportedExtensions)
		{
			var theCreatedInterface = headerFactory.CreateEmptyUnsupported();
						theCreatedInterface.UnsupportedExtensions = @unsupportedExtensions;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspUserAgentHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="value">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspUserAgentHeader" /> with populated values.
		/// </returns>
		public static IRtspUserAgentHeader CreateUserAgent(
			this IRtspHeaderFactory headerFactory,
			string? @value)
		{
			var theCreatedInterface = headerFactory.CreateEmptyUserAgent();
						theCreatedInterface.Value = @value;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspViaHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="value">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspViaHeader" /> with populated values.
		/// </returns>
		public static IRtspViaHeader CreateVia(
			this IRtspHeaderFactory headerFactory,
			List<ViaRecord> @value)
		{
			var theCreatedInterface = headerFactory.CreateEmptyVia();
						theCreatedInterface.Value = @value;
			return theCreatedInterface;
		}

				/// <summary>
		///   Creates an <see cref="IRtspWwwAuthenticateHeader" /> instance and populates it with given value..
		/// </summary>
		/// <param name="digestRealm">The parameter.</param>
		/// <param name="nonce">The parameter.</param>
		/// <param name="algorithm">The parameter.</param>
		/// <returns>
		///   <see cref="IRtspWwwAuthenticateHeader" /> with populated values.
		/// </returns>
		public static IRtspWwwAuthenticateHeader CreateWwwAuthenticate(
			this IRtspHeaderFactory headerFactory,
			string? @digestRealm, string? @nonce, string? @algorithm)
		{
			var theCreatedInterface = headerFactory.CreateEmptyWwwAuthenticate();
						theCreatedInterface.DigestRealm = @digestRealm;
			theCreatedInterface.Nonce = @nonce;
			theCreatedInterface.Algorithm = @algorithm;
			return theCreatedInterface;
		}

			}
}

namespace ContentDotNet.Protocols.Rtsp.Headers.Impl.Base
{
	internal abstract class AcceptBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class AcceptCredentialsBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class AcceptEncodingBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class AcceptLanguageBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class AcceptRangesBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class AllowBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class AuthenticationInfoBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class AuthorizationBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class BlockSizeBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class CacheControlBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ConnectionBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ContentBaseBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ContentEncodingBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ContentLanguageBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ContentLengthBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ContentLocationBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class CSeqBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class DateBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ExpiresBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class FromBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class IfMatchBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class IfModifiedSinceBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class IfNoneMatchBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class LastModifiedBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class LocationBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class MediaPropertiesBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class MediaRangeBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class MTagBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class NotifyReasonBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class PipelinedRequestsBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ProxyAuthenticateBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ProxyAuthenticationInfoBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ProxyAuthorizationBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ProxyRequireBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ProxySupportedBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class PublicBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class RangeBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ReferrerBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class RequestStatusBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class RequireBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class RetryAfterBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class RtpInfoBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ScaleBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class SeekStyleBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ServerBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class SessionBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class SpeedBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class SupportedBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class TerminateReasonBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class TimestampBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class TransportBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class UnsupportedBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class UserAgentBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class ViaBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

	internal abstract class WwwAuthenticateBase : IRtspHeader
	{
		public abstract string Text { get; }
	}

}

