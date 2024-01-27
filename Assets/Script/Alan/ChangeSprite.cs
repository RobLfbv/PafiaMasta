using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite newSprite;
    public bool displayNew;

    private Sprite oldSprite;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
       sr = GetComponent<SpriteRenderer>();
       sr.sprite = oldSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (displayNew)
        {
            sr.sprite = newSprite;
        }
        else sr.sprite = oldSprite;
    }

    // c'est clairement pas le plus opti mais j'ai pensé à ça pour afficher l'usine quand elle explose et aussi le perso couvert de ketchup

    //ya juste à récup et à activer le displayNew du script pour passer au nouveau sprite


    // {\_/}
    // (^_^)
    // / > Alan le S
}
