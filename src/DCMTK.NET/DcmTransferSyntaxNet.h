#pragma once

public enum class TransferSyntaxNet
{
	/// unknown transfer syntax or dataset created in-memory
	Unknown = -1,
	/// Implicit VR Little Endian
	LittleEndianImplicit = 0,
	/// Implicit VR Big Endian (pseudo transfer syntax that does not really exist)
	BigEndianImplicit = 1,
	/// Explicit VR Little Endian
	LittleEndianExplicit = 2,
	/// Explicit VR Big Endian
	BigEndianExplicit = 3,
	/// JPEG Baseline (lossy)
	JPEGProcess1TransferSyntax = 4,
	/// JPEG Extended Sequential (lossy, 8/12 bit)
	JPEGProcess2_4TransferSyntax = 5,
	/// JPEG Extended Sequential (lossy, 8/12 bit), arithmetic coding
	JPEGProcess3_5TransferSyntax = 6,
	/// JPEG Spectral Selection, Non-Hierarchical (lossy, 8/12 bit)
	JPEGProcess6_8TransferSyntax = 7,
	/// JPEG Spectral Selection, Non-Hierarchical (lossy, 8/12 bit), arithmetic coding
	JPEGProcess7_9TransferSyntax = 8,
	/// JPEG Full Progression, Non-Hierarchical (lossy, 8/12 bit)
	JPEGProcess10_12TransferSyntax = 9,
	/// JPEG Full Progression, Non-Hierarchical (lossy, 8/12 bit), arithmetic coding
	JPEGProcess11_13TransferSyntax = 10,
	/// JPEG Lossless with any selection value
	JPEGProcess14TransferSyntax = 11,
	/// JPEG Lossless with any selection value, arithmetic coding
	JPEGProcess15TransferSyntax = 12,
	/// JPEG Extended Sequential, Hierarchical (lossy, 8/12 bit)
	JPEGProcess16_18TransferSyntax = 13,
	/// JPEG Extended Sequential, Hierarchical (lossy, 8/12 bit), arithmetic coding
	JPEGProcess17_19TransferSyntax = 14,
	/// JPEG Spectral Selection, Hierarchical (lossy, 8/12 bit)
	JPEGProcess20_22TransferSyntax = 15,
	/// JPEG Spectral Selection, Hierarchical (lossy, 8/12 bit), arithmetic coding
	JPEGProcess21_23TransferSyntax = 16,
	/// JPEG Full Progression, Hierarchical (lossy, 8/12 bit)
	JPEGProcess24_26TransferSyntax = 17,
	/// JPEG Full Progression, Hierarchical (lossy, 8/12 bit), arithmetic coding
	JPEGProcess25_27TransferSyntax = 18,
	/// JPEG Lossless, Hierarchical
	JPEGProcess28TransferSyntax = 19,
	/// JPEG Lossless, Hierarchical, arithmetic coding
	JPEGProcess29TransferSyntax = 20,
	/// JPEG Lossless, Selection Value 1
	JPEGProcess14SV1TransferSyntax = 21,
	/// Run Length Encoding (lossless)
	RLELossless = 22,
	/// JPEG-LS (lossless)
	JPEGLSLossless = 23,
	/// JPEG-LS (lossless or near-lossless mode)
	JPEGLSLossy = 24,
	/// Deflated Explicit VR Little Endian
	DeflatedLittleEndianExplicit = 25,
	/// JPEG 2000 (lossless)
	JPEG2000LosslessOnly = 26,
	/// JPEG 2000 (lossless or lossy)
	JPEG2000 = 27,
	/// MPEG2 Main Profile at Main Level
	MPEG2MainProfileAtMainLevel = 28,
	/// MPEG2 Main Profile at High Level
	MPEG2MainProfileAtHighLevel = 29,
	/// JPEG 2000 part 2 multi-component extensions (lossless)
	JPEG2000MulticomponentLosslessOnly = 30,
	/// JPEG 2000 part 2 multi-component extensions (lossless or lossy)
	JPEG2000Multicomponent = 31,
	/// JPIP Referenced
	JPIPReferenced = 32,
	/// JPIP Referenced Deflate
	JPIPReferencedDeflate = 33
};

public ref class DcmTransferSyntaxNet
{
public:
};

