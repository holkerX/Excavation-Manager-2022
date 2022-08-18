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
        public Texture2D cursorTextureTrowel;
        public Texture2D cursorTextureDefault;

        public Texture2D cursorTextureHoverArtifactStop;
        public Texture2D cursorTextureHoverArtifactSelect;

        public Texture2D cursorTextureZoomArtifact;

        private Texture2D cursorTexture;

        private Vector2 cursorHotspot;

        private bool inspectArtifact;

        public bool MouseHoversToolbox;

        void Awake()
        {
            cursorTexture = cursorTextureDefault;
        }

        public void setCursor()
        {
            cursorHotspot =
                new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
            Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
            MouseHoversToolbox = false;
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
            
            MouseHoversToolbox = true;
        }

        public void setCursorArtifact()
        {
            if (inspectArtifact)
            {
                cursorHotspot =
                new Vector2(cursorTextureHoverArtifactSelect.width / 2,
                    cursorTextureHoverArtifactSelect.height / 2);
                Cursor
                    .SetCursor(cursorTextureHoverArtifactSelect,
                    cursorHotspot,
                    CursorMode.Auto);
            }
            else
            {
                cursorHotspot =
                    new Vector2(cursorTextureHoverArtifactStop.width / 2,
                        cursorTextureHoverArtifactStop.height / 2);
                Cursor
                    .SetCursor(cursorTextureHoverArtifactStop,
                    cursorHotspot,
                    CursorMode.Auto);
            }
            MouseHoversToolbox = false;
        }

        public void setCursorShovel()
        {
            cursorTexture = cursorTextureShovel;
            inspectArtifact = false;
        }

        public void setCursorPickaxe()
        {
            cursorTexture = cursorTexturePickaxe;
            inspectArtifact = false;
        }

        public void setCursorTrowel()
        {
            cursorTexture = cursorTextureTrowel;
            inspectArtifact = false;
        }

        public void setCursorZoomArtifact()
        {
            cursorTexture = cursorTextureZoomArtifact;
            inspectArtifact = true;
        }
    }
}
