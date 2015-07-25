using UnityEngine;
using System.Collections;

public class GameUnit : MonoBehaviour {

    protected MeshRenderer meshRenderer;

    protected Material mat_default;
    protected Material mat_selected;
    protected Material mat_ended;

    protected Shader shader_selected;
    protected Shader shader_default;

    protected string _name;
    protected bool _turnOver = false;
    protected bool _selected = false;
    protected int _team;

    protected bool _canMove = false;
    protected bool _canAttack = false;
    protected bool _canUsePowers = false;

    protected Tile _currentTile;

    public string Name
    {
        get { return _name; }
    }

    public Tile CurrentTile
    {
        get { return _currentTile; }
        set { _currentTile = value; }
    }

    public int Team
    {
        get { return _team; }
        set { _team = value; }
    }

    public bool TurnOver
    {
        get { return _turnOver; }
        set
        {
            _turnOver = value;
            if (_turnOver) meshRenderer.material = mat_ended;
            else meshRenderer.material = mat_default;
        }
    }

    public bool Selected
    {
        get { return _selected; }
        set
        {
            _selected = value;
            if (_selected) meshRenderer.material.shader = shader_selected;
            else meshRenderer.material.shader = shader_default;
        }
    }

    protected virtual void Init()
    {
        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        mat_ended = Resources.Load<Material>("Materials/Unit_Turn_Over");
        //LoadResources();
    }

    public void Select()
    {
        _selected = true;
        meshRenderer.material.shader = shader_selected;
    }

    public void Deselect()
    {
        _selected = false;
        meshRenderer.material.shader = shader_default;
    }

    public virtual void SnapToCurrent()
    {
        Transform[] trans = _currentTile.gameObject.GetComponentsInChildren<Transform>();
        Transform point = null;
        for (int i = 0; i < trans.Length; i++)
        {
            if (trans[i].name == "p_piece_center")
                point = trans[i];
        }
        if (point)
        {
            Vector3 off = new Vector3(0.0f, 0.5f, 0.0f);
            gameObject.transform.position = point.position + off;
        }
    }
}
