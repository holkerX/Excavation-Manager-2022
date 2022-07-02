using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CursorBehavior
{
    public class DiggingCursorBehaviour : MonoBehaviour
    {
        //Cursor textures
        public Texture2D cursorTextureShovel;

        public Texture2D cursorTexturePickaxe;

        public Texture2D cursorTextureDefault;

        public Texture2D cursorTextureHoverArtifact;

        public Texture2D cursorTextureZoomArtifact;

        private Texture2D cursorTexture;

        private Vector2 cursorHotspot;

        void Awake()
        {
            cursorTexture = cursorTextureDefault;
        }

        public void setCursor()
        {
            cursorHotspot =
                new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
            Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
        }

        public void setCursorDefault()
        {
            cursorHotspot =
                new Vector2(cursorTextureDefault.width / 2,
                    cursorTextureDefault.height / 2);
            Cursor
                .SetCursor(cursorTextureDefault,
                cursorHotspot,
                CursorMode.Auto);
        }

        public void setCursorArtifact()
        {
            cursorHotspot =
                new Vector2(cursorTextureHoverArtifact.width / 2,
                    cursorTextureHoverArtifact.height / 2);
            Cursor
                .SetCursor(cursorTextureHoverArtifact,
                cursorHotspot,
                CursorMode.Auto);
        }

        public void setCursorShovel()
        {
            cursorTexture = cursorTextureShovel;
        }

        public void setCursorPickaxe()
        {
            cursorTexture = cursorTexturePickaxe;
        }

        public void setCursorZoomArtifact()
        {
            cursorTexture = cursorTextureZoomArtifact;
        }
    }
}
