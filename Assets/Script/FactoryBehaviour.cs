using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FactoryBehaviour : MonoBehaviour
{
    [Header("Up Cauldrons")]
    public GameObject parentCauldronUp;
    public List<GameObject> UpCauldrons;
    public List<GameObject> SpoonUpCauldrons;
    public List<GameObject> KeyUpCauldrons;
    public bool moveUpCauldron = true;
    public int indexUp = 0;

    [Header("Down Cauldrons")]
    public GameObject parentCauldronDown;
    public List<GameObject> DownCauldrons;
    public List<GameObject> SpoonDownCauldrons;
    public List<GameObject> KeyDownCauldrons;
    public bool moveDownCauldron = true;
    public int indexDown = -1;


    [Header("Right Cauldrons")]
    public GameObject parentCauldronRight;
    public List<GameObject> RightCauldrons;
    public List<GameObject> SpoonRightCauldrons;
    public List<GameObject> KeyRightCauldrons;
    public bool moveRightCauldron = true;
    public int indexRight = -1;


    [Header("Left Cauldrons")]
    public GameObject parentCauldronLeft;
    public List<GameObject> LeftCauldrons;
    public List<GameObject> KeyLeftCauldrons;
    public List<GameObject> SpoonLeftCauldrons;
    public bool moveLeftCauldron = true;
    public int indexLeft = -1;


    [Header("Spoon Rotation")]
    public Vector3 initPos = new Vector3(0, 0, 90f);
    public Vector3 goToPos = new Vector3(0, 0, 115f);

    [Header("Fire")]
    [SerializeField]
    private GameObject[] fires;
    private int indexFire = 0;
    [Header("Color")]
    private Color color1 = new Color(0.8f, 0.8f, 0.8f);
    private Color color2 = new Color(0.5f, 0.5f, 0.5f);
    private Color color3 = new Color(0.34f, 0.34f, 0.34f);
    private Color color4 = new Color(0.12f, 0.12f, 0.12f);
    private Color color5 = new Color(0, 0, 0);

    private void OnEnable()
    {
        RightCauldrons = new List<GameObject>();
        KeyRightCauldrons = new List<GameObject>();
        SpoonRightCauldrons = new List<GameObject>();

        LeftCauldrons = new List<GameObject>();
        KeyLeftCauldrons = new List<GameObject>();
        SpoonLeftCauldrons = new List<GameObject>();

        UpCauldrons = new List<GameObject>();
        KeyUpCauldrons = new List<GameObject>();
        SpoonUpCauldrons = new List<GameObject>();

        DownCauldrons = new List<GameObject>();
        KeyDownCauldrons = new List<GameObject>();
        SpoonDownCauldrons = new List<GameObject>();
        foreach (Transform gb in parentCauldronUp.GetComponentsInChildren<Transform>(true))
        {
            if (gb.name.Equals("AnchorSpoon"))
            {
                SpoonUpCauldrons.Add(gb.gameObject);
            }
            else if (gb.name.Contains("CauldronUp"))
            {
                UpCauldrons.Add(gb.gameObject);
            }
            else if (gb.name.Equals("KeyToPress"))
            {
                KeyUpCauldrons.Add(gb.gameObject);
            }
            else if (gb.name.Equals("KeyKeyboard"))
            {
                GameStateBehaviour.Instance.UIKeyboard.Add(gb.gameObject);
            }
            else if (gb.name.Equals("KeyGamepad"))
            {
                GameStateBehaviour.Instance.UIGamepad.Add(gb.gameObject);
            }
        }
        foreach (Transform gb in parentCauldronRight.GetComponentsInChildren<Transform>(true))
        {
            if (gb.name.Equals("AnchorSpoon"))
            {
                SpoonRightCauldrons.Add(gb.gameObject);
            }
            else if (gb.name.Contains("CauldronRight"))
            {
                RightCauldrons.Add(gb.gameObject);
            }
            else if (gb.name.Equals("KeyToPress"))
            {
                KeyRightCauldrons.Add(gb.gameObject);
            }
            else if (gb.name.Equals("KeyKeyboard"))
            {
                GameStateBehaviour.Instance.UIKeyboard.Add(gb.gameObject);
            }
            else if (gb.name.Equals("KeyGamepad"))
            {
                GameStateBehaviour.Instance.UIGamepad.Add(gb.gameObject);
            }
        }
        foreach (Transform gb in parentCauldronLeft.GetComponentsInChildren<Transform>(true))
        {
            if (gb.name.Equals("AnchorSpoon"))
            {
                SpoonLeftCauldrons.Add(gb.gameObject);
            }
            else if (gb.name.Contains("CauldronLeft"))
            {
                LeftCauldrons.Add(gb.gameObject);
            }
            else if (gb.name.Equals("KeyToPress"))
            {
                KeyLeftCauldrons.Add(gb.gameObject);
            }
            else if (gb.name.Equals("KeyKeyboard"))
            {
                GameStateBehaviour.Instance.UIKeyboard.Add(gb.gameObject);
            }
            else if (gb.name.Equals("KeyGamepad"))
            {
                GameStateBehaviour.Instance.UIGamepad.Add(gb.gameObject);
            }
        }
        foreach (Transform gb in parentCauldronDown.GetComponentsInChildren<Transform>(true))
        {
            if (gb.name.Equals("AnchorSpoon"))
            {
                SpoonDownCauldrons.Add(gb.gameObject);
            }
            else if (gb.name.Contains("CauldronDown"))
            {
                DownCauldrons.Add(gb.gameObject);
            }
            else if (gb.name.Equals("KeyToPress"))
            {
                KeyDownCauldrons.Add(gb.gameObject);
            }
            else if (gb.name.Equals("KeyKeyboard"))
            {
                GameStateBehaviour.Instance.UIKeyboard.Add(gb.gameObject);
            }
            else if (gb.name.Equals("KeyGamepad"))
            {
                GameStateBehaviour.Instance.UIGamepad.Add(gb.gameObject);
            }
        }
        moveDownCauldron = false;
        StartCoroutine(DoAfterDelay(5.5f, () =>
        {
            ActivateDown();
            moveDownCauldron = true;
        }));
        moveUpCauldron = true;
        moveRightCauldron = false;
        StartCoroutine(DoAfterDelay(8.5f, () =>
        {
            ActivateRight();
            moveRightCauldron = true;
        }));
        moveLeftCauldron = false;
        StartCoroutine(DoAfterDelay(13.5f, () =>
        {
            ActivateLeft();
            moveLeftCauldron = true;
        }));

        StartCoroutine(DoAfterDelay(8f, () =>
        {
            ActivateNextFire();
        }));

        StartCoroutine(DoAfterDelay(15f, () =>
        {
            ActivateNextFire();
            ActivateNextFire();
        }));

        StartCoroutine(DoAfterDelay(20f, () =>
        {
            ActivateNextFire();
            ActivateNextFire();
            ActivateNextFire();
        }));

        StartCoroutine(DoAfterDelay(25f, () =>
        {
            ActivateNextFire();
            ActivateNextFire();
            ActivateNextFire();
        }));

        StartCoroutine(DoAfterDelay(30f, EndMiniGame));
    }

    public void MoveLeftCauldrons()
    {
        moveLeftCauldron = false;
        foreach (GameObject gb in KeyLeftCauldrons)
            gb.SetActive(false);
        ActivateLeft();
        StartCoroutine(DoAfterDelay(1.5f, () =>
        {
            moveLeftCauldron = true;
            foreach (GameObject gb in KeyLeftCauldrons)
                gb.SetActive(true);
        }));
        foreach (GameObject gb in SpoonLeftCauldrons)
        {
            gb.transform.DOLocalRotate(goToPos, 0.2f).OnComplete(() =>
            {
                gb.transform.DOLocalRotate(initPos, 0.2f);
            });
        }
    }

    public void MoveRightCauldrons()
    {
        moveRightCauldron = false;
        foreach (GameObject gb in KeyRightCauldrons)
            gb.SetActive(false);
        ActivateRight();
        StartCoroutine(DoAfterDelay(1.5f, () =>
        {
            moveRightCauldron = true;
            foreach (GameObject gb in KeyRightCauldrons)
                gb.SetActive(true);
        }));
        foreach (GameObject gb in SpoonRightCauldrons)
        {
            gb.transform.DOLocalRotate(goToPos, 0.2f).OnComplete(() =>
            {
                gb.transform.DOLocalRotate(initPos, 0.2f);
            });
        }
    }

    public void MoveUpCauldrons()
    {
        moveUpCauldron = false;
        foreach (GameObject gb in KeyUpCauldrons)
            gb.SetActive(false);
        ActivateUp();
        StartCoroutine(DoAfterDelay(1.5f, () =>
        {
            moveUpCauldron = true;
            foreach (GameObject gb in KeyUpCauldrons)
                gb.SetActive(true);
        }));
        foreach (GameObject gb in SpoonUpCauldrons)
        {
            gb.transform.DOLocalRotate(goToPos, 0.2f).OnComplete(() =>
            {
                gb.transform.DOLocalRotate(initPos, 0.2f);
            });
        }
    }

    public void MoveDownCauldrons()
    {
        moveDownCauldron = false;
        foreach (GameObject gb in KeyDownCauldrons)
            gb.SetActive(false);
        ActivateDown();
        StartCoroutine(DoAfterDelay(1.5f, () =>
        {
            moveDownCauldron = true;
            foreach (GameObject gb in KeyDownCauldrons)
                gb.SetActive(true);
        }));
        foreach (GameObject gb in SpoonDownCauldrons)
        {
            gb.transform.DOLocalRotate(goToPos, 0.2f).OnComplete(() =>
            {
                gb.transform.DOLocalRotate(initPos, 0.2f);
            });
        }

    }

    public void ActivateLeft()
    {
        indexLeft++;
        if (indexLeft < LeftCauldrons.Count)
        {
            LeftCauldrons[indexLeft].SetActive(true);
            Transform forwardChaudron = LeftCauldrons[indexLeft].transform.Find("Cauldron").Find("Front");
            Transform sauce = LeftCauldrons[indexLeft].transform.Find("Cauldron").Find("Sauce");
            Transform backChaudron = LeftCauldrons[indexLeft].transform.Find("Cauldron").Find("Back");


            DG.Tweening.Sequence mySequence1 = DOTween.Sequence();
            mySequence1.Append(forwardChaudron.transform.DOShakePosition(6f, 1, 10));
            mySequence1.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 1, 10));
            mySequence1.Insert(0, backChaudron.transform.DOShakePosition(6f, 1, 10));
            mySequence1.Insert(0, backChaudron.transform.DOShakeRotation(6f, 1, 10));
            mySequence1.Insert(0, sauce.transform.DOShakePosition(6f, 1, 10));
            mySequence1.Insert(0, sauce.transform.DOShakeRotation(6f, 1, 10));
            mySequence1.Insert(0, sauce.GetComponent<Image>().DOColor(color1, 6));

            DG.Tweening.Sequence mySequence2 = DOTween.Sequence();
            mySequence2.Append(forwardChaudron.transform.DOShakePosition(6f, 15, 10));
            mySequence2.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 15, 10));
            mySequence2.Insert(0, backChaudron.transform.DOShakePosition(6f, 15, 10));
            mySequence2.Insert(0, backChaudron.transform.DOShakeRotation(6f, 15, 10));
            mySequence2.Insert(0, sauce.transform.DOShakePosition(6f, 15, 10));
            mySequence2.Insert(0, sauce.transform.DOShakeRotation(6f, 15, 10));
            mySequence2.Insert(0, sauce.GetComponent<Image>().DOColor(color3, 6));
            mySequence2.PrependInterval(4f);

            DG.Tweening.Sequence mySequence3 = DOTween.Sequence();
            mySequence3.Append(forwardChaudron.transform.DOShakePosition(6f, 30, 10));
            mySequence3.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 30, 10));
            mySequence3.Insert(0, backChaudron.transform.DOShakePosition(6f, 30, 10));
            mySequence3.Insert(0, backChaudron.transform.DOShakeRotation(6f, 30, 10));
            mySequence3.Insert(0, sauce.transform.DOShakePosition(6f, 30, 10));
            mySequence3.Insert(0, sauce.transform.DOShakeRotation(6f, 30, 10));
            mySequence3.Insert(0, sauce.GetComponent<Image>().DOColor(color4, 6));
            mySequence3.PrependInterval(8f);

            DG.Tweening.Sequence mySequence4 = DOTween.Sequence();
            mySequence4.Append(forwardChaudron.transform.DOShakePosition(6f, 60, 10));
            mySequence4.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 60, 10));
            mySequence4.Insert(0, backChaudron.transform.DOShakePosition(6f, 60, 10));
            mySequence4.Insert(0, backChaudron.transform.DOShakeRotation(6f, 60, 10));
            mySequence4.Insert(0, sauce.transform.DOShakePosition(6f, 60, 10));
            mySequence4.Insert(0, sauce.transform.DOShakeRotation(6f, 60, 10));
            mySequence4.Insert(0, sauce.GetComponent<Image>().DOColor(color5, 6));
            mySequence4.PrependInterval(12f);

            mySequence1.OnComplete(() => mySequence2.Play());
            mySequence2.OnComplete(() => mySequence3.Play());
            mySequence3.OnComplete(() => mySequence4.Play());
            mySequence4.OnComplete(() => mySequence4.Play());

            mySequence1.Play();
        }
    }
    public void ActivateRight()
    {
        indexRight++;
        if (indexRight < RightCauldrons.Count)
        {
            RightCauldrons[indexRight].SetActive(true);

            Transform forwardChaudron = RightCauldrons[indexRight].transform.Find("Cauldron").Find("Front");
            Transform sauce = RightCauldrons[indexRight].transform.Find("Cauldron").Find("Sauce");
            Transform backChaudron = RightCauldrons[indexRight].transform.Find("Cauldron").Find("Back");


            DG.Tweening.Sequence mySequence1 = DOTween.Sequence();
            mySequence1.Append(forwardChaudron.transform.DOShakePosition(6f, 1, 10));
            mySequence1.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 1, 10));
            mySequence1.Insert(0, backChaudron.transform.DOShakePosition(6f, 1, 10));
            mySequence1.Insert(0, backChaudron.transform.DOShakeRotation(6f, 1, 10));
            mySequence1.Insert(0, sauce.transform.DOShakePosition(6f, 1, 10));
            mySequence1.Insert(0, sauce.transform.DOShakeRotation(6f, 1, 10));
            mySequence1.Insert(0, sauce.GetComponent<Image>().DOColor(color1, 6));

            DG.Tweening.Sequence mySequence2 = DOTween.Sequence();
            mySequence2.Append(forwardChaudron.transform.DOShakePosition(6f, 15, 10));
            mySequence2.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 15, 10));
            mySequence2.Insert(0, backChaudron.transform.DOShakePosition(6f, 15, 10));
            mySequence2.Insert(0, backChaudron.transform.DOShakeRotation(6f, 15, 10));
            mySequence2.Insert(0, sauce.transform.DOShakePosition(6f, 15, 10));
            mySequence2.Insert(0, sauce.transform.DOShakeRotation(6f, 15, 10));
            mySequence2.Insert(0, sauce.GetComponent<Image>().DOColor(color3, 6));
            mySequence2.PrependInterval(4f);

            DG.Tweening.Sequence mySequence3 = DOTween.Sequence();
            mySequence3.Append(forwardChaudron.transform.DOShakePosition(6f, 30, 10));
            mySequence3.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 30, 10));
            mySequence3.Insert(0, backChaudron.transform.DOShakePosition(6f, 30, 10));
            mySequence3.Insert(0, backChaudron.transform.DOShakeRotation(6f, 30, 10));
            mySequence3.Insert(0, sauce.transform.DOShakePosition(6f, 30, 10));
            mySequence3.Insert(0, sauce.transform.DOShakeRotation(6f, 30, 10));
            mySequence3.Insert(0, sauce.GetComponent<Image>().DOColor(color4, 6));
            mySequence3.PrependInterval(8f);

            DG.Tweening.Sequence mySequence4 = DOTween.Sequence();
            mySequence4.Append(forwardChaudron.transform.DOShakePosition(6f, 60, 10));
            mySequence4.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 60, 10));
            mySequence4.Insert(0, backChaudron.transform.DOShakePosition(6f, 60, 10));
            mySequence4.Insert(0, backChaudron.transform.DOShakeRotation(6f, 60, 10));
            mySequence4.Insert(0, sauce.transform.DOShakePosition(6f, 60, 10));
            mySequence4.Insert(0, sauce.transform.DOShakeRotation(6f, 60, 10));
            mySequence4.Insert(0, sauce.GetComponent<Image>().DOColor(color5, 6));
            mySequence4.PrependInterval(12f);

            mySequence1.OnComplete(() => mySequence2.Play());
            mySequence2.OnComplete(() => mySequence3.Play());
            mySequence3.OnComplete(() => mySequence4.Play());
            mySequence4.OnComplete(() => mySequence4.Play());

            mySequence1.Play();
        }
    }
    public void ActivateUp()
    {
        indexUp++;
        if (indexUp < UpCauldrons.Count)
        {
            UpCauldrons[indexUp].SetActive(true);
            Transform forwardChaudron = UpCauldrons[indexUp].transform.Find("Cauldron").Find("Front");
            Transform sauce = UpCauldrons[indexUp].transform.Find("Cauldron").Find("Sauce");
            Transform backChaudron = UpCauldrons[indexUp].transform.Find("Cauldron").Find("Back");


            DG.Tweening.Sequence mySequence1 = DOTween.Sequence();
            mySequence1.Append(forwardChaudron.transform.DOShakePosition(6f, 1, 10));
            mySequence1.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 1, 10));
            mySequence1.Insert(0, backChaudron.transform.DOShakePosition(6f, 1, 10));
            mySequence1.Insert(0, backChaudron.transform.DOShakeRotation(6f, 1, 10));
            mySequence1.Insert(0, sauce.transform.DOShakePosition(6f, 1, 10));
            mySequence1.Insert(0, sauce.transform.DOShakeRotation(6f, 1, 10));
            mySequence1.Insert(0, sauce.GetComponent<Image>().DOColor(color1, 6));

            DG.Tweening.Sequence mySequence2 = DOTween.Sequence();
            mySequence2.Append(forwardChaudron.transform.DOShakePosition(6f, 15, 10));
            mySequence2.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 15, 10));
            mySequence2.Insert(0, backChaudron.transform.DOShakePosition(6f, 15, 10));
            mySequence2.Insert(0, backChaudron.transform.DOShakeRotation(6f, 15, 10));
            mySequence2.Insert(0, sauce.transform.DOShakePosition(6f, 15, 10));
            mySequence2.Insert(0, sauce.transform.DOShakeRotation(6f, 15, 10));
            mySequence2.Insert(0, sauce.GetComponent<Image>().DOColor(color3, 6));
            mySequence2.PrependInterval(4f);

            DG.Tweening.Sequence mySequence3 = DOTween.Sequence();
            mySequence3.Append(forwardChaudron.transform.DOShakePosition(6f, 30, 10));
            mySequence3.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 30, 10));
            mySequence3.Insert(0, backChaudron.transform.DOShakePosition(6f, 30, 10));
            mySequence3.Insert(0, backChaudron.transform.DOShakeRotation(6f, 30, 10));
            mySequence3.Insert(0, sauce.transform.DOShakePosition(6f, 30, 10));
            mySequence3.Insert(0, sauce.transform.DOShakeRotation(6f, 30, 10));
            mySequence3.Insert(0, sauce.GetComponent<Image>().DOColor(color4, 6));
            mySequence3.PrependInterval(8f);


            DG.Tweening.Sequence mySequence4 = DOTween.Sequence();
            mySequence4.Append(forwardChaudron.transform.DOShakePosition(6f, 60, 10));
            mySequence4.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 60, 10));
            mySequence4.Insert(0, backChaudron.transform.DOShakePosition(6f, 60, 10));
            mySequence4.Insert(0, backChaudron.transform.DOShakeRotation(6f, 60, 10));
            mySequence4.Insert(0, sauce.transform.DOShakePosition(6f, 60, 10));
            mySequence4.Insert(0, sauce.transform.DOShakeRotation(6f, 60, 10));
            mySequence4.Insert(0, sauce.GetComponent<Image>().DOColor(color5, 6));
            mySequence4.PrependInterval(12f);

            mySequence1.OnComplete(() => mySequence2.Play());
            mySequence2.OnComplete(() => mySequence3.Play());
            mySequence3.OnComplete(() => mySequence4.Play());
            mySequence4.OnComplete(() => mySequence4.Play());

            mySequence1.Play();
        }
    }
    public void ActivateDown()
    {
        indexDown++;
        if (indexDown < DownCauldrons.Count)
        {
            DownCauldrons[indexDown].SetActive(true);
            Transform forwardChaudron = DownCauldrons[indexDown].transform.Find("Cauldron").Find("Front");
            Transform sauce = DownCauldrons[indexDown].transform.Find("Cauldron").Find("Sauce");
            Transform backChaudron = DownCauldrons[indexDown].transform.Find("Cauldron").Find("Back");


            DG.Tweening.Sequence mySequence1 = DOTween.Sequence();
            mySequence1.Append(forwardChaudron.transform.DOShakePosition(6f, 1, 10));
            mySequence1.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 1, 10));
            mySequence1.Insert(0, backChaudron.transform.DOShakePosition(6f, 1, 10));
            mySequence1.Insert(0, backChaudron.transform.DOShakeRotation(6f, 1, 10));
            mySequence1.Insert(0, sauce.transform.DOShakePosition(6f, 1, 10));
            mySequence1.Insert(0, sauce.transform.DOShakeRotation(6f, 1, 10));
            mySequence1.Insert(0, sauce.GetComponent<Image>().DOColor(color1, 6));

            DG.Tweening.Sequence mySequence2 = DOTween.Sequence();
            mySequence2.Append(forwardChaudron.transform.DOShakePosition(6f, 15, 10));
            mySequence2.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 15, 10));
            mySequence2.Insert(0, backChaudron.transform.DOShakePosition(6f, 15, 10));
            mySequence2.Insert(0, backChaudron.transform.DOShakeRotation(6f, 15, 10));
            mySequence2.Insert(0, sauce.transform.DOShakePosition(6f, 15, 10));
            mySequence2.Insert(0, sauce.transform.DOShakeRotation(6f, 15, 10));
            mySequence2.Insert(0, sauce.GetComponent<Image>().DOColor(color3, 6));
            mySequence2.PrependInterval(6f);

            DG.Tweening.Sequence mySequence3 = DOTween.Sequence();
            mySequence3.Append(forwardChaudron.transform.DOShakePosition(6f, 30, 10));
            mySequence3.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 30, 10));
            mySequence3.Insert(0, backChaudron.transform.DOShakePosition(6f, 30, 10));
            mySequence3.Insert(0, backChaudron.transform.DOShakeRotation(6f, 30, 10));
            mySequence3.Insert(0, sauce.transform.DOShakePosition(6f, 30, 10));
            mySequence3.Insert(0, sauce.transform.DOShakeRotation(6f, 30, 10));
            mySequence3.Insert(0, sauce.GetComponent<Image>().DOColor(color4, 6));
            mySequence3.PrependInterval(12f);

            DG.Tweening.Sequence mySequence4 = DOTween.Sequence();
            mySequence4.Append(forwardChaudron.transform.DOShakePosition(6f, 60, 10));
            mySequence4.Insert(0, forwardChaudron.transform.DOShakeRotation(6f, 60, 10));
            mySequence4.Insert(0, backChaudron.transform.DOShakePosition(6f, 60, 10));
            mySequence4.Insert(0, backChaudron.transform.DOShakeRotation(6f, 60, 10));
            mySequence4.Insert(0, sauce.transform.DOShakePosition(6f, 60, 10));
            mySequence4.Insert(0, sauce.transform.DOShakeRotation(6f, 60, 10));
            mySequence4.Insert(0, sauce.GetComponent<Image>().DOColor(color5, 6));
            mySequence4.PrependInterval(18f);

            mySequence1.OnComplete(() => mySequence2.Play());
            mySequence2.OnComplete(() => mySequence3.Play());
            mySequence3.OnComplete(() => mySequence4.Play());
            mySequence4.OnComplete(() => mySequence4.Play());

            mySequence1.Play();
        }
    }

    public void ActivateNextFire()
    {
        if (indexFire < fires.Length)
        {
            fires[indexFire].SetActive(true);
            indexFire++;
        }
    }

    public IEnumerator DoAfterDelay(float delaySeconds, System.Action thingToDo)
    {
        yield return new WaitForSeconds(delaySeconds);
        if (GameStateBehaviour.Instance.currentState == GameStateBehaviour.GameState.FactoryMiniGame)
        {
            thingToDo();
        }
    }

    public void EndMiniGame()
    {
        PlayerPrefs.SetInt("UsineFire", 1);
        GameStateBehaviour.Instance.ChangementUsine();
        GameStateBehaviour.Instance.ChangeToDialogue();
        DialogueBox.Instance.currentDialogue = GameStateBehaviour.Instance.zilyInteraction.GetComponent<DialogueInteractionBehaviour>().LoseDialogue;
        GameStateBehaviour.Instance.ChangeZilyDialogue(3);
        GameStateBehaviour.Instance.player.toInteract = GameStateBehaviour.Instance.zilyInteraction.GetComponent<DialogueInteractionBehaviour>();
        GameStateBehaviour.Instance.canMove = true;
        DialogueBox.Instance.setOriginalText();
    }

}
