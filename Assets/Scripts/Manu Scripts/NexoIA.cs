﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexoIA : MonoBehaviour {

    public Unidad unit;

    private void Start()
    {
        unit = this.gameObject.GetComponent<Unidad>();
    }

    public void Act()
    {
        Hex hex = this.transform.parent.GetComponent<Hex>();
        Hex placement = AdaptedMap.Instance.map[hex.tileX - 1, hex.tileY - 1];
        Unidad u = unit.BuildUnit(SelectionManager.Instance.Worker, placement, unit.Owner.Pueblo);
        unit.finished = true;
        GameManager.Instance.CPU.Squad.Add(u);
        GameManager.Instance.CheckPlayerTurn();
    }
}
