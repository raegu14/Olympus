using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {

  //camera
  int CAP = 10;
  Camera cam;
  public float DELAY_TIMER = 1f; // delay
  int iter = 1;
  float z = -10;
  float timer;
  float x;
  float leftOffset = 5f;
  float rightOffset = -5f;
  float yOffset = 1f;
  char anchor = 'l';
  bool track = true;
  bool zooming = false;
  float zoomLevel = 5f; // smaller is closer
  float zoomSpeed = 1;
  int zoomDirection = 1; // positive = zoomout

  //player
  public GameObject player;
  Player p;

  public GameObject menu;


  // Use this for initialization
  void Start()
  {
    cam = GetComponent<Camera>();
    p = player.GetComponent<Player>();
    timer = 0;
    if(GameObject.Find("Title") != null)
    {
      Vector3 pos = GameObject.Find("Title").transform.position;
      cam.transform.position = new Vector3(pos.x, 4.25f, -10f);
      track = false;
    }
    else
    {
      cam.transform.position = player.transform.position + new Vector3(leftOffset, 0, 0);
    }
  }

  // Update is called once per frame
  void FixedUpdate ()
  {
    if(track)
    {
      x = cam.transform.position.x;
      if(Input.GetKey(KeyCode.D))
      {
        if(anchor != 'l') // need to start moving camera
        {
          anchor = 'l';
          timer = DELAY_TIMER;
          iter = 1;
        }
        else
        {
          if(timer > 0)
            timer -= Time.deltaTime;
          else
          {
            x += ShiftCap(iter)*leftOffset;
            iter++;
          }
        }
      }
      else if(Input.GetKey(KeyCode.A))
      {
        if(anchor != 'r')
        {
          anchor = 'r';
          timer = DELAY_TIMER;
          iter = 1;
        }
        else
        {
          if(timer > 0)
            timer -= Time.deltaTime;
          else
          {
            x += ShiftCap(iter)*rightOffset;
            iter++;
          }
        }
      }
      else if(!p.move)
      {
        if(anchor == 'l')
          x += ShiftCap(iter)*leftOffset;
        else
          x += ShiftCap(iter)*rightOffset;
      }
      // Clamp camera
      if(x > player.transform.position.x + leftOffset)
        x = player.transform.position.x + leftOffset;
      else if(x < player.transform.position.x + rightOffset)
        x = player.transform.position.x + rightOffset;
      cam.transform.position = new Vector3(x, player.transform.position.y + yOffset, z);
    }
    if(zooming)
    {
      cam.orthographicSize += Time.deltaTime*zoomSpeed*zoomDirection;
      float increment = Time.deltaTime*zoomSpeed*zoomDirection*2;
      menu.transform.localScale += new Vector3(increment, increment, 0);
      if(zoomDirection == 1 && cam.orthographicSize >= zoomLevel
        || zoomDirection == -1 && cam.orthographicSize <= zoomLevel)
      {
        cam.orthographicSize = zoomLevel;
        zooming = false;
      }
    }
  }

  float ShiftCap(int iter)
  {
    float t = Time.deltaTime;
    int i = (iter <= CAP? iter : CAP);
    return Mathf.Log(i)*t;
  }

  // Public functions
  public void Zoom(float zoom)
  {
    float diff = zoom - zoomLevel;
    if(diff == 0)
      return;
    zoomDirection = (diff < 0 ? -1 : 1);
    zoomLevel = zoom;
    zooming = true;
  }

  public void SetZoom(float zoom)
  {
    cam.orthographicSize = zoom;
    float diff = zoom - zoomLevel;
    if(diff == 0)
      return;
    zoomDirection = (diff < 0 ? -1 : 1);
    zoomLevel = zoom;
    float temp = diff;
    while(zoomDirection == 1 && temp < zoomLevel ||
          zoomDirection == -1 && temp > zoomLevel)
    {
      float increment = 0.01f*zoomSpeed*zoomDirection;
      menu.transform.localScale += new Vector3(increment*2, increment*2, 0);
      temp += increment;
    }
  }

  public void Track()
  {
    track = true;
  }

}
