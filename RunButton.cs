using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RunButton : MonoBehaviour
{
    public Vector2 Step1Move;
    public GameObject Step2HardDrive;
    public Vector2 Step3Move;
    public Vector2 Step4Move;
    public Vector2 Step6Move;
    public Vector2 Step9Move;


    public SpriteRenderer spriteRenderer;
    private SpriteRenderer personSpriteRenderer;
    private Animator personAnimator;
    public Animator robotAnimator;
    public Sprite greenButtonTexture;
    public Sprite redButtonTexture;
    public Sprite runButtonTexture;
    public SpriteRenderer computerScreen;
    public SpriteRenderer towerBlocks;

    public SpriteRenderer progress1;
    public SpriteRenderer progress2;
    public SpriteRenderer progress3;


    public bool isRunning = false;
    private MovementScript movementScript;

    [Header("Instruction Text")]
    public TextMeshProUGUI instructionText;
    [TextArea(3, 10)]
    public string instruction1;
    [TextArea(3, 10)]
    public string instruction2;
    [TextArea(3, 10)]
    public string instruction3;
    [TextArea(3, 10)]
    public string instruction4;
    [TextArea(3, 10)]
    public string instruction5;
    [TextArea(3, 10)]
    public string instruction6;
    [TextArea(3, 10)]
    public string instruction7;
    [TextArea(3, 10)]
    public string instruction8;

    private void Start()
    {
        movementScript = FindObjectOfType<MovementScript>();
        personSpriteRenderer = movementScript.gameObject.GetComponent<SpriteRenderer>();
        personAnimator = movementScript.gameObject.GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        var levelController = FindObjectOfType<LevelController>();
        var valid = levelController.IsCorrectCommand();
        if(valid)
        {
            spriteRenderer.sprite = greenButtonTexture;

            FindObjectOfType<AudioController>().PlaySuccessFX();
            StartCoroutine(RunStep(levelController.currentStep));
            levelController.currentStep += 1;
            levelController.ResetAllBlocks();
        } else
        {
            FindObjectOfType<AudioController>().PlayFailureFX();
            spriteRenderer.sprite = redButtonTexture;
            Invoke(nameof(ResetButton), 2.0f);
            levelController.ResetAllBlocks();
        }
    }

    private void ResetButton()
    {
        spriteRenderer.sprite = runButtonTexture;
    }

    private void SetText(string text)
    {
        instructionText.text = text;
    }
    private void SetProgress(int progress) {
        switch(progress)
        {
            case 0:
                progress1.enabled = false;
                progress2.enabled = false;
                progress3.enabled = false;
                break;
            case 1:
                progress1.enabled = true;
                progress2.enabled = false;
                progress3.enabled = false;
                break;
            case 2:
                progress1.enabled = false;
                progress2.enabled = true;
                progress3.enabled = false;
                break;
            case 3:
                progress1.enabled = false;
                progress2.enabled = false;
                progress3.enabled = true;
                break;
        }
    }

    private IEnumerator RunStep(int step)
    {
        Debug.Log("Running step " + (step + 1));
        switch (step + 1)
        {
            case 1:
                yield return Step1();
                break;
            case 2:
                yield return Step2();
                break;
            case 3:
                yield return Step3();
                break;
            case 4:
                yield return Step4();
                break;
            case 5:
                yield return Step5();
                break;
            case 6:
                yield return Step6();
                break;
            case 7:
                yield return Step7();
                break;
            case 8:
                yield return Step8();
                break;
            case 9:
                yield return Step9();
                break;
            case 10:
                yield return Step10();
                break;
        }

        ResetButton();
    }

    private IEnumerator Step1() {
        personSpriteRenderer.flipX = true;
        personAnimator.Play("Base Layer.C-Run");
        yield return movementScript.MoveTo(
            Step1Move,
            1.8f
        );
        personAnimator.Play("Base Layer.C-Idle-Animation");
        SetText(instruction2);
    }
    private IEnumerator Step2()
    {
        Destroy(Step2HardDrive);
        personAnimator.Play("Base Layer.C-JumpBest");
        yield return new WaitForSeconds(1.16f);
        SetText(instruction3);
    }
    private IEnumerator Step3()
    {
        personSpriteRenderer.flipX = false;
        personAnimator.Play("Base Layer.C-Runharddrive");
        yield return movementScript.MoveTo(
            Step3Move,
            3.5f
        );
        personAnimator.Play("Base Layer.C-Idle-Animation");
        robotAnimator.Play("Base Layer.Robot-Wake");
        SetProgress(1);
        SetText(instruction4);
    }

    private IEnumerator Step4()
    {
        personAnimator.Play("Base Layer.C-Run");
        yield return movementScript.MoveTo(
            Step4Move,
            1.8f
        );
        personAnimator.Play("Base Layer.C-Idle-Animation");
    }

    private IEnumerator Step5()
    {
        personAnimator.Play("Base Layer.C-PushButton-Animation");
        yield return new WaitForSeconds(0.58f);
        robotAnimator.Play("Base Layer.RobotHappy");
        SetProgress(2);
        SetText(instruction5);
        yield return new WaitForSeconds(0.58f);
    }

    private IEnumerator Step6()
    {
        personSpriteRenderer.flipX = true;
        personAnimator.Play("Base Layer.C-Run");
        yield return movementScript.MoveTo(
            Step6Move,
            2.0f
        );
        personAnimator.Play("Base Layer.C-Idle-Animation");
    }

    private IEnumerator Step7()
    {
        personAnimator.Play("Base Layer.C-PushButton-Animation");
        yield return new WaitForSeconds(1.16f);
        computerScreen.enabled = true;
        SetText(instruction6);
    }

    private IEnumerator Step8()
    {
        yield return new WaitForSeconds(1.0f);
        SetProgress(3);
        SetText(instruction7);
    }

    private IEnumerator Step9()
    {
        personSpriteRenderer.flipX = false;
        personAnimator.Play("Base Layer.C-Run");
        yield return movementScript.MoveTo(
            Step9Move,
            2.0f
        );
        personAnimator.Play("Base Layer.C-Idle-Animation");
    }

    private IEnumerator Step10()
    {
        personAnimator.Play("Base Layer.C-PushButton-Animation");
        yield return new WaitForSeconds(1.16f);
        towerBlocks.enabled = false;
        robotAnimator.Play("Base Layer.RobotTowerBuild");
        personAnimator.Play("Base Layer.C-Happy");
        SetText(instruction8);
        yield return new WaitForSeconds(4.0f);

        FindObjectOfType<MainController>().TransitionToQuitScene();

    }
}