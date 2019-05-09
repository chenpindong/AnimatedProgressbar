using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class AnimatedProgressbar : MonoBehaviour
{
    private enum ChangeType
    {
        Add, //增加
        Sub, //减少
    }

    [SerializeField] private RawImage m_barUI = null;
    [SerializeField] private float m_fillSpeed = 0;
    [SerializeField] [Range(0, 1)] private float m_fillAmount = 1;
    [SerializeField] private bool m_autoPlay = true;
    [SerializeField] private float m_currentAmount = 0;
    [SerializeField] private float m_initUvRectWidth = 15;
    [SerializeField] private float m_speed = 0;

    private bool m_isPlaying = false;
    private ChangeType m_changeType = ChangeType.Add;

    private float CurrentAmount
    {
        get { return m_currentAmount; }
        set
        {
            m_currentAmount = m_changeType == ChangeType.Add ? Mathf.Min(value, m_fillAmount) : Mathf.Max(value, m_fillAmount);
            var localScale = m_barUI.rectTransform.localScale;
            localScale.x = m_currentAmount;
            m_barUI.rectTransform.localScale = localScale;

            var rect = m_barUI.uvRect;
            rect.width = m_initUvRectWidth * m_currentAmount;
            m_barUI.uvRect = rect;
        }
    }

    public float FillAmount
    {
        get
        {
            return m_fillAmount;
        }
        set
        {
            m_changeType = value >= m_currentAmount ? ChangeType.Add : ChangeType.Sub;
            m_fillAmount = Mathf.Clamp01(value);
            m_isPlaying = true;
        }
    }

#if UNITY_EDITOR
    void OnValidate()//在Inspector中修改参数值，就会自动调用这个方法
    {
        if (Application.isPlaying)
        {
            m_changeType = m_fillAmount >= m_currentAmount ? ChangeType.Add : ChangeType.Sub;
            m_isPlaying = true;
        }
        else
        {
            CurrentAmount = m_fillAmount;
        }
    }
#endif

    private void Start()
    {
        m_changeType = ChangeType.Add;
        if (m_autoPlay)
        {
            m_currentAmount = 0;
            m_isPlaying = true;
        }
        else
        {
            CurrentAmount = m_fillAmount;
            m_isPlaying = false;
        }
    }

    private void Update()
    {
        var rect = m_barUI.uvRect;
        rect.x -= m_speed;
        m_barUI.uvRect = rect;

        if (m_isPlaying)
        {
            if (m_changeType == ChangeType.Add)
            {
                m_currentAmount += m_fillSpeed;
                CurrentAmount = m_currentAmount;

                if (m_currentAmount >= m_fillAmount)
                {
                    m_isPlaying = false;
                }
            }
            else
            {
                m_currentAmount -= m_fillSpeed;
                CurrentAmount = m_currentAmount;

                if (m_currentAmount <= m_fillAmount)
                {
                    m_isPlaying = false;
                }
            }
        }
    }
}
