using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class Reskin : MonoBehaviour
{
    private DBPersonagens DBPersonagens;
    private SpriteRenderer sRender;
    private Image imageMap;

    public Sprite[] sprites;
    public string spriteSheetName;
    public string loadesSpriteSheetName;

    private Dictionary<string, Sprite> spriteSheet;

    public bool isMap; 

    private void Start()
    {
        DBPersonagens = FindObjectOfType<DBPersonagens>();

        if (DBPersonagens == null)
        {
            return;
        }

        spriteSheetName = DBPersonagens.spriteSheetName[DBPersonagens.idPersonagem].name;

        if (!isMap)
        {
            sRender = GetComponentInChildren<SpriteRenderer>();
        }
        else
        {
            imageMap = GetComponent<Image>();   
        }

        LoadSpriteSheet();
    }

    private void LateUpdate()
    {
        if (DBPersonagens == null)
        {
            return;
        }

        if (DBPersonagens.idPersonagem != DBPersonagens.idPersonagemAtual)
        {
            spriteSheetName = DBPersonagens.spriteSheetName[DBPersonagens.idPersonagem].name;
            DBPersonagens.idPersonagemAtual = DBPersonagens.idPersonagem;
            LoadSpriteSheet();
        }

        if (spriteSheet != null)
        {
            if (!isMap)
            {
                sRender.sprite = spriteSheet[sRender.sprite.name];
            }
            else
            {
                imageMap.sprite = spriteSheet[imageMap.sprite.name];
            }
        }
    }

    private void LoadSpriteSheet()
    {
        sprites = Resources.LoadAll<Sprite>(spriteSheetName);

        spriteSheet = sprites.ToDictionary(x => x.name, x => x);
        loadesSpriteSheetName = spriteSheetName;
    }
}
