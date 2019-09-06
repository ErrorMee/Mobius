
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
#endif

public class CullImage : Image
{
    [Range(0, 1)]
    [SerializeField]
    private float m_CullTop = 0.25f;
    public float CullTop { get { return m_CullTop; } set { m_CullTop = Mathf.Clamp01(value); SetVerticesDirty(); } }
    [Range(0, 1)]
    [SerializeField]
    private float m_CullBottom = 0.25f;
    public float CullBottom { get { return m_CullBottom; } set { m_CullBottom = Mathf.Clamp01(value); SetVerticesDirty(); } }
    [Range(0, 1)]
    [SerializeField]
    private float m_CullLeft = 0.25f;
    public float CullLeft { get { return m_CullLeft; } set { m_CullLeft = Mathf.Clamp01(value); SetVerticesDirty(); } }
    [Range(0, 1)]
    [SerializeField]
    private float m_CullRight = 0.25f;
    public float CullRight { get { return m_CullRight; } set { m_CullRight = Mathf.Clamp01(value); SetVerticesDirty(); } }

    [SerializeField]
    private Color32 m_ColorLeft = Color.white;
    public Color32 ColorLeft { get { return m_ColorLeft; } set { m_ColorLeft = value; SetVerticesDirty(); } }

    [SerializeField]
    private Color32 m_ColorRight = Color.white;
    public Color32 ColorRight { get { return m_ColorRight; } set { m_ColorRight = value; SetVerticesDirty(); } }

    public bool m_SupportBlank = true;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        QuadFill(vh);
    }

    private void QuadFill(VertexHelper toFill)
    {
        Vector4 atlasUV = (overrideSprite != null) ? UnityEngine.Sprites.DataUtility.GetOuterUV(overrideSprite) : Vector4.zero;
        float w = atlasUV.z - atlasUV.x;
        float h = atlasUV.w - atlasUV.y;

        toFill.Clear();

        Vector2 maskSize = new Vector2(rectTransform.sizeDelta.x / 2, rectTransform.sizeDelta.y / 2);
        Color vertexColor = (sprite == null && !m_SupportBlank) ? Color.clear : this.color;

        var lb = UIVertex.simpleVert;
        lb.position = new Vector3(-maskSize.x * (1 - m_CullLeft * 2), -maskSize.y * (1 - m_CullBottom * 2), 0);
        lb.uv0 = new Vector2(atlasUV.x + w * (m_CullLeft), atlasUV.y + h * (m_CullBottom));
        lb.color = ColorLeft == Color.white ? vertexColor : vertexColor * ColorLeft;

        var lt = UIVertex.simpleVert;
        lt.position = new Vector3(-maskSize.x * (1 - m_CullLeft * 2), maskSize.y * (1 - m_CullTop * 2), 0);
        lt.uv0 = new Vector2(atlasUV.x + w * (m_CullLeft), atlasUV.w - h * m_CullTop);
        lt.color = ColorLeft == Color.white ? vertexColor : vertexColor * ColorLeft;

        var rt = UIVertex.simpleVert;
        rt.position = new Vector3(maskSize.x * (1 - m_CullRight * 2), maskSize.y * (1 - m_CullTop * 2), 0);
        rt.uv0 = new Vector2(atlasUV.z - w * (m_CullRight), atlasUV.w - h * m_CullTop);
        rt.color = ColorRight == Color.white ? vertexColor : vertexColor * ColorRight;

        var rb = UIVertex.simpleVert;
        rb.position = new Vector3(maskSize.x * (1 - m_CullRight * 2), -maskSize.y * (1 - m_CullBottom * 2), 0);
        rb.uv0 = new Vector2(atlasUV.z - w * (m_CullRight), atlasUV.y + h * (m_CullBottom));
        rb.color = ColorRight == Color.white ? vertexColor : vertexColor * ColorRight;

        toFill.AddVert(lb);
        toFill.AddVert(lt);
        toFill.AddVert(rt);
        toFill.AddVert(rb);

        toFill.AddTriangle(0, 1, 2);
        toFill.AddTriangle(2, 3, 0);
    }


    private Vector4 GetDrawingDimensions(bool shouldPreserveAspect)
    {
        var padding = sprite == null ? Vector4.zero : UnityEngine.Sprites.DataUtility.GetPadding(sprite);
        var size = sprite == null ? Vector2.zero : new Vector2(sprite.rect.width, sprite.rect.height);

        Rect r = GetPixelAdjustedRect();
        int spriteW = Mathf.RoundToInt(size.x);
        int spriteH = Mathf.RoundToInt(size.y);
        var v = new Vector4(
                padding.x / spriteW,
                padding.y / spriteH,
                (spriteW - padding.z) / spriteW,
                (spriteH - padding.w) / spriteH);

        if (shouldPreserveAspect && size.sqrMagnitude > 0.0f)
        {
            var spriteRatio = size.x / size.y;
            var rectRatio = r.width / r.height;

            if (spriteRatio > rectRatio)
            {
                var oldHeight = r.height;
                r.height = r.width * (1.0f / spriteRatio);
                r.y += (oldHeight - r.height) * rectTransform.pivot.y;
            }
            else
            {
                var oldWidth = r.width;
                r.width = r.height * spriteRatio;
                r.x += (oldWidth - r.width) * rectTransform.pivot.x;
            }
        }

        v = new Vector4(
                r.x + r.width * v.x,
                r.y + r.height * v.y,
                r.x + r.width * v.z,
                r.y + r.height * v.w
                );

        return v;
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(CullImage), true)]
public class CullImageEditor : GraphicEditor
{
    SerializedProperty m_Sprite;
    GUIContent m_SpriteContent;

    SerializedProperty m_CullTop;
    GUIContent m_CullTopContent;
    SerializedProperty m_CullBottom;
    GUIContent m_CullBottomContent;
    SerializedProperty m_CullLeft;
    GUIContent m_CullLeftContent;
    SerializedProperty m_CullRight;
    GUIContent m_CullRightContent;

    SerializedProperty m_ColorLeft;
    SerializedProperty m_ColorRight;

    SerializedProperty m_SupportBlank;

    protected override void OnEnable()
    {
        base.OnEnable();

        m_SpriteContent = new GUIContent("Source Image");

        m_Sprite = serializedObject.FindProperty("m_Sprite");

        m_CullTop = serializedObject.FindProperty("m_CullTop");
        m_CullBottom = serializedObject.FindProperty("m_CullBottom");
        m_CullLeft = serializedObject.FindProperty("m_CullLeft");
        m_CullRight = serializedObject.FindProperty("m_CullRight");

        m_ColorLeft = serializedObject.FindProperty("m_ColorLeft");
        m_ColorRight = serializedObject.FindProperty("m_ColorRight");

        m_SupportBlank = serializedObject.FindProperty("m_SupportBlank");

        SetShowNativeSize(true);
    }

    protected override void OnDisable()
    {

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SpriteGUI();
        AppearanceControlsGUI();
        RaycastControlsGUI();
        SetShowNativeSize(false);
        NativeSizeButtonGUI();

        MaskGUI();

        serializedObject.ApplyModifiedProperties();
    }

    void SetShowNativeSize(bool instant)
    {
        Image.Type type = Image.Type.Filled;
        bool showNativeSize = (type == Image.Type.Simple || type == Image.Type.Filled) && m_Sprite.objectReferenceValue != null;
        base.SetShowNativeSize(showNativeSize, instant);
    }

    protected void SpriteGUI()
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(m_Sprite, m_SpriteContent);
        EditorGUI.EndChangeCheck();
    }

    protected void MaskGUI()
    {
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(m_CullTop);
        EditorGUILayout.PropertyField(m_CullBottom);
        EditorGUILayout.PropertyField(m_CullLeft);
        EditorGUILayout.PropertyField(m_CullRight);

        EditorGUILayout.PropertyField(m_ColorLeft);
        EditorGUILayout.PropertyField(m_ColorRight);

        EditorGUILayout.PropertyField(m_SupportBlank);

        EditorGUI.EndChangeCheck();
    }

    [MenuItem("GameObject/UI/CullImage")]
    public static void CreateCullImage()
    {
        var goRoot = Selection.activeGameObject;
        if (goRoot == null)
            return;
        var polygon = new GameObject("CullImage");
        polygon.AddComponent<CullImage>();
        polygon.transform.SetParent(goRoot.transform, false);
        polygon.transform.SetAsLastSibling();
    }
}
#endif
