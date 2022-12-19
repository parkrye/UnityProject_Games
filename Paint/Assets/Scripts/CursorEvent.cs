using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorEvent : MonoBehaviour
{
    Vector3 mousePos;
    RaycastHit2D hit;
    public Brush brush;
    public SelectFill selFill;
    public SelectBrush selBrush;
    public Eraser eraser;
    public Save save;
    public int mode;

    private void Start()
    {
        mode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag != "Canvas")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.gameObject.tag == "Palette")
                    {
                        hit.collider.gameObject.GetComponent<Palette>().UsePalette();
                        selFill.Selected(brush.GetInk());
                        selBrush.Selected(brush.GetInk());
                    }
                    else if (hit.collider.gameObject.tag == "Eraser")
                        eraser.Erase();
                    else if (hit.collider.gameObject.tag == "SelFill")
                    {
                        mode = 1;
                        selFill.Selected(brush.GetInk());
                        brush.ChangeBrushType(mode);
                    }
                    else if (hit.collider.gameObject.tag == "SelBrush")
                    {
                        mode = 0;
                        selBrush.Selected(brush.GetInk());
                        brush.ChangeBrushType(mode);
                    }
                    else if (hit.collider.gameObject.tag == "Save")
                    {
                        save.SaveImage();
                    }
                    else if (hit.collider.gameObject.tag == "Quit")
                    {
                        Application.Quit(0);
                    }
                }
            }
            else
            {
                if (mode == 0)
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (hit.collider.gameObject.tag == "Canvas")
                            hit.collider.gameObject.GetComponent<Canvas>().Paint(brush.GetInk());
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.collider.gameObject.tag == "Canvas")
                            selFill.Fill(hit.collider.gameObject.GetComponent<Canvas>(), brush);
                    }
                }
            }
        }
    }
}
