using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

// 复制到项目 UI\ 目录，将命名空间改为项目名。
namespace JiaTaiMark
{
    public enum UiIconKind
    {
        OpenTemplate,
        WebApi,
        SystemSettings
    }

    /// <summary>
    /// 统一加载 skills\libs\icons 下的界面图标，运行时优先使用输出目录 Assets\icons。
    /// </summary>
    public static class UiIconHelper
    {
        public const string SkillsIconDir = @"C:\Users\SZJCZ\.cursor\skills\libs\icons";

        private static readonly string[] IconFiles =
        {
            "open_template.png",
            "web_api.png",
            "system_settings.png"
        };

        public static string GetFileName(UiIconKind kind)
        {
            return IconFiles[(int)kind];
        }

        public static string ResolveIconPath(UiIconKind kind)
        {
            string fileName = GetFileName(kind);
            string localPath = Path.Combine(Application.StartupPath, "Assets", "icons", fileName);
            if (File.Exists(localPath))
            {
                return localPath;
            }

            string skillsPath = Path.Combine(SkillsIconDir, fileName);
            if (File.Exists(skillsPath))
            {
                return skillsPath;
            }

            return localPath;
        }

        public static Image LoadIcon(UiIconKind kind, int size = 32)
        {
            string path = ResolveIconPath(kind);
            if (!File.Exists(path))
            {
                return null;
            }

            using (Image src = Image.FromFile(path))
            {
                return new Bitmap(src, new Size(size, size));
            }
        }

        public static void ApplyImageButton(Button button, UiIconKind kind, ToolTip toolTip, string tipText, int size = 32)
        {
            Image icon = LoadIcon(kind, size);
            button.Image = icon;
            button.Text = string.Empty;
            button.TextImageRelation = TextImageRelation.Overlay;
            button.ImageAlign = ContentAlignment.MiddleCenter;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 238, 252);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 220, 245);
            button.BackColor = SystemColors.Control;
            button.Size = new Size(size + 12, size + 12);
            button.Padding = new Padding(4);
            button.UseVisualStyleBackColor = true;

            if (toolTip != null && !string.IsNullOrEmpty(tipText))
            {
                toolTip.SetToolTip(button, tipText);
            }
        }
    }
}
