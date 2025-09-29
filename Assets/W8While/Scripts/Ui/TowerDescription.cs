using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerDescription : MonoBehaviour
{
    [SerializeField] private TMP_Text _towerDescription;
    static public TMP_Text towerDescription;
    [SerializeField] private Image _firstSkillImage;
    static public Image firstSkillImage;
    [SerializeField] private Image _secondSkillImage;
    static public Image secondSkillImage;
    [SerializeField] private TMP_Text _firstSkillText;
    static public TMP_Text firstSkillText;
    [SerializeField] private TMP_Text _secondSkillText;
    static public TMP_Text secondSkillText;
    [SerializeField] private Slider _expireanceSlider;
    static public Slider expireanceSlider;
    [SerializeField] private TMP_Text _currentLevel;
    static public TMP_Text currentLevel;
    [SerializeField] private TMP_Text _currentExp;
    static public TMP_Text currentExp;
    static public TowerClickHandler SelectedTower;


    private void Start()
    {
        towerDescription = _towerDescription;
        firstSkillImage = _firstSkillImage;
        secondSkillImage = _secondSkillImage;
        firstSkillText = _firstSkillText;
        secondSkillText = _secondSkillText;
        expireanceSlider = _expireanceSlider;
        currentLevel = _currentLevel;
        currentExp = _currentExp;
        gameObject.SetActive(false);
    }



    static public void ChangeTowerDescription(TowerObjects tower, TowerClickHandler towerCliclHandler)
    {
        SelectedTower = towerCliclHandler;
        ExpireanceTower towerExp = towerCliclHandler.GetComponent<ExpireanceTower>();
        expireanceSlider.value = (float)towerExp.CurrentExp / (float)towerExp.NeedExp;
        currentExp.text = $"{towerExp.CurrentExp}/{towerExp.NeedExp}";
        currentLevel.text = towerExp.CurrentLevel.ToString();
        TowerStats _towerStats = towerCliclHandler.GetComponent<TowerStats>();
        towerDescription.text = $"Damage = {_towerStats.CurrentDamage} \nAttack Speed = {_towerStats.CurrentAttackSpeed} \nAttackRange = {_towerStats.CurrentAttackRange}";
        firstSkillImage.sprite = tower.firstSkill.sprite;
        firstSkillText.text = tower.firstSkill.SkillDescription(towerCliclHandler);
        if (tower.secondSkill == null)
        {
            secondSkillImage.sprite = null;
            secondSkillText.text = null;
            return;
        }
        secondSkillImage.sprite = tower.secondSkill.sprite;
        secondSkillText.text = tower.secondSkill.SkillDescription(towerCliclHandler);
    }
}
