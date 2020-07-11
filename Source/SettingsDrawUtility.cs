using HugsLib.Settings;
using RimWorld;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Hotseat
{
    class SettingsDrawUtility
    {
        //private const float ContentPadding = 5f;
        //private const float IconSize = 32f;
        //private const float IconGap = 1f;
        //private const float TextMargin = 20f;
        //private const float BottomMargin = 2f;
        //private static Color background = new Color(0.5f, 0, 0, 0.1f);

        internal static bool StorytellerCheckboxList(Rect rect, SettingHandle<StorytellerUsageSetting> storytellerUsageSetting, IEnumerable<StorytellerDef> storytellers)
        {
            bool changed = false;

            Color save = GUI.color;
            GUI.color = new Color(0.5f, 0, 0, 0.1f);
            GUI.DrawTexture(rect, TexUI.FastFillTex);
            GUI.color = save;

            rect.height = storytellers.Count() * 32;
            storytellerUsageSetting.CustomDrawerHeight = rect.height;

            foreach (var storytellerItem in storytellers.Select((value, i) => new {i, value })) {
                if (!storytellerUsageSetting.Value.dictionary.ContainsKey(storytellerItem.value.defName))
                {
                    //Widgets.Label(new Rect(), new GUIContent(storyteller.portraitTinyTex));
                    storytellerUsageSetting.Value.dictionary.Add(storytellerItem.value.defName, true);
                }

                GenUI.SetLabelAlign(TextAnchor.UpperLeft);
                
                Rect storytellerRow = new Rect(rect);
                storytellerRow.position = new Vector2(rect.position.x, rect.yMin + (storytellerItem.i * 32));
                storytellerRow.height = 32;

                //save = GUI.color;
                //GUI.color = new Color(float.Parse("0."+storytellerItem.i), 0, 0, 0.1f);
                //GUI.DrawTexture(storytellerRow, TexUI.FastFillTex);
                //GUI.color = save;


                GUI.color = Color.white;

                //GUI.DrawTexture(storytellerRow, storytellerItem.value.portraitTinyTex);


                Widgets.Label(storytellerRow, storytellerItem.value.defName);

                bool selectFlag = true;
                bool temp = storytellerUsageSetting.Value.dictionary[storytellerItem.value.defName];


                //Widgets.CheckboxLabeled(storytellerRow, storytellerItem.value.defName, ref temp);

                /*if (Widgets.CheckboxLabeled(storytellerRow, storytellerItem.value.defName, ref temp))
                {
                    storytellerUsageSetting.Value.dictionary.SetOrAdd(storytellerItem.value.defName, !storytellerUsageSetting.Value.dictionary[storytellerItem.value.defName]);
                    Log.Warning(storytellerUsageSetting.Value.ToString());
                    changed = true;
                }*/

                
                GenUI.ResetLabelAlign();

                Rect button = new Rect(rect.RightHalf());
                GUI.color = storytellerUsageSetting.Value.dictionary[storytellerItem.value.defName] ? new Color(.5f, 1f, .5f) : new Color(1f, .5f, .5f);
                button.height = 32;
                button.position = new Vector2(rect.RightHalf().position.x, rect.yMin + (storytellerItem.i * 32));

                if (Widgets.ButtonText(button, storytellerUsageSetting.Value.dictionary[storytellerItem.value.defName].ToString()))
                {
                    storytellerUsageSetting.Value.dictionary.SetOrAdd(storytellerItem.value.defName, !storytellerUsageSetting.Value.dictionary[storytellerItem.value.defName]);
                    Log.Warning(storytellerUsageSetting.Value.ToString());
                    changed = true;
                }


                GUI.color = Color.white;
                

                /*bool bla = storytellerUsageDictionary.Value.dictionary[storyteller.defName];
                     bool defaultBool = false;
                     Widgets.CheckboxLabeled(rect, storyteller.defName, ref bla);

                     var labelRect = new Rect(rect);
                     GUI.DrawTexture(labelRect, TexUI.GrayTextBG);*/

                //Widgets.Label(labelRect, storyteller.defName);

                //    Settings.GetHandle<bool>(
                //"enableStorytellerSwitching",
                //"enableStorytellerSwitching_title".Translate(),
                //"enableStorytellerSwitching_desc".Translate(),
                //false);

            }




            /*            foreach (KeyValuePair<string, bool> entry in storytellerUsageDictionary.Value.dictionary) {

                        }*/

            return changed;
            //return true;
        }
    }
    }

