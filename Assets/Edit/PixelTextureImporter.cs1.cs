using UnityEditor;
using UnityEngine;

public sealed class PixelTextureImporter : AssetPostprocessor
{
    private const string TargetFolder1 =
        "Assets/Art Assets/Generic-Character/";

    private const string TargetFolder2 =
        "Assets/Art Assets/Legacy-Fantasy-High Forest/";

    private void OnPreprocessTexture()
    {
        if (!assetPath.StartsWith(TargetFolder1, System.StringComparison.Ordinal) &&
            !assetPath.StartsWith(TargetFolder2, System.StringComparison.Ordinal))
        {
            return;
        }

        var importer = (TextureImporter)assetImporter;
        importer.spritePixelsPerUnit = 16;
        importer.wrapMode = TextureWrapMode.Clamp;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
    }
}