using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "BoxType2", menuName = "BoxType2/AddBoxBox")]
public class AddBoxBoxesSO : BoxType2SO
{
    // Start is called before the first frame update


    public enum AddBoxDir
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
    public AddBoxDir addBoxDir;

    public override void Init(Transform transform)
    {
        base.Init(transform);
        Debug.Log(transform);
        AddBarrelBtn.addBoxBtnPress.AddListener(OnAddBoxBtnPress);
        Event.OnDoneMoveBarrel.AddListener(ShowIndicator);
        Event.OnDoneSpawnBarrel.AddListener(ShowIndicator);

    }

    private void ShowIndicator()
    {
        if (HaveBoxAtAllDir())
        {
            transform.Find("SpecialIndicator").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("SpecialIndicator").gameObject.SetActive(false);
        }
    }

    public override void OnDisable()
    {
        Debug.Log("DISABLE PLS");
        AddBarrelBtn.addBoxBtnPress.RemoveListener(OnAddBoxBtnPress);
        Event.OnDoneMoveBarrel.RemoveListener(ShowIndicator);
        Event.OnDoneSpawnBarrel.RemoveListener(ShowIndicator);
    }

    public void OnAddBoxBtnPress()
    {
        if (HaveBoxAtAllDir())
        {

            Vector3 checkDir = Vector3.zero;
            switch (addBoxDir)
            {
                case AddBoxBoxesSO.AddBoxDir.Up:
                    checkDir = Vector3.up;
                    break;
                case AddBoxBoxesSO.AddBoxDir.Down:
                    checkDir = Vector3.down;


                    break;
                case AddBoxBoxesSO.AddBoxDir.Left:
                    checkDir = Vector3.left;


                    break;
                case AddBoxBoxesSO.AddBoxDir.Right:
                    checkDir = Vector3.right;
                    break;
                default:
                    break;
            }
            base.transform.DOPunchScale(new Vector3(2, 2, 0), 1, 1, 1).OnComplete(() =>
            {
                base.transform.DOScale(new Vector3(1, 1, 1), 1);
            });
            SpawnMoreBarrelCommand(checkDir);

        }
    }
    bool HaveBoxAtAllDir()
    {
        // AddBoxBoxesSO addBoxBoxesSO = _boxType2SO as AddBoxBoxesSO;

        return CheckHaveBarrelAtDir(Vector3.up) || CheckHaveBarrelAtDir(Vector3.down) || CheckHaveBarrelAtDir(Vector3.left) || CheckHaveBarrelAtDir(Vector3.right);
    }
    bool CheckHaveBarrelAtDir(Vector3 checkDir)
    {
        Node checkNode = GridEditManager.instance.GetNodeByPos(transform.position - checkDir);
        if (checkNode == null) return false;
        if (!checkNode.IsOccupied()) return false;
        if (checkNode.GetOccupiedBox2()?.GetBoxType() != BoxType2.barrel) return false;
        return true;
        // if (checkNode.GetOccupiedBox2()?.GetBoxType() != BoxType2.barrel) return;
        // if (Input.GetKeyDown(KeyCode.Q))
        // {

        //     GridEditManager.OnSpawnMoreBarrel.Invoke(Vector3.right);
        // }


    }
    void SpawnMoreBarrelCommand(Vector3 spawnDir)
    {
        ICommand command = new AddBarrelCommand(spawnDir);
        CommandScheduler.ScheduleCommand(command);
        CommandScheduler.Execute();

    }

}
