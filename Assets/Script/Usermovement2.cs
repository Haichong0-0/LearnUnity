using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Usermovement2 : MonoBehaviour
{   
    Vector2 Player_position, position_jump, position_collide, position_lift, position_keyfollow,camerapos;
    float rad,reachlength,initialx,initialy;
    Vector2 wd,Dash_direction;
    string a,no;
    
    
    float distance,raylength;
    float tempx,tempy,temp;
    float nextdash=0,dash_cd = 0.1f;
    int X_direction,num, theta= 55,lastfacing;//-1:left,1:right
    int Y_direction, wallfacing;//-1: down, 1: up
    int berrycount=0;
    float stamina;
    
    Rigidbody2D rb;
    Transform tf;
    bool onground, onwall,canchange,jumping,candash = true, wallgrab = false;
    public bool havekey =false, haveberry = false,alive = true;
    bool playertop, playerbottom;
    Vector2 contactpoint, rb_position, xboxSize, yboxSize,poz, scaleChange,direcitonup, direcitondown;
    
    public GameObject Staminab;
    
    GameObject colItem,colItemy,stabar;
    [SerializeField] LayerMask enviroment ;
    [SerializeField] Transform xboxCorner;
    [SerializeField] Transform yboxCorner;
    LayerMask Enviroment = 64, Player =8;
    [SerializeField] float speed =80f, jump_speed=2000f, dash_force = 3500f, pushforce,stamint =10f;
    [SerializeField] int gravity=10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
		Player_position =rb.position;

        no = PlayerPrefs.GetString("loadno");//get current save
        initialx = PlayerPrefs.GetFloat("lastposx"+no);
        initialy =  PlayerPrefs.GetFloat("lastposy"+no);
        rb.position = new Vector2(initialx,initialy); //last respawn point


        scaleChange = rb.transform.localScale;//determin scale of object
        xboxSize =2*xboxCorner.localPosition*scaleChange;//horizontal detection box
        yboxSize =2*yboxCorner.localPosition*scaleChange* new Vector2(1,-1); //vertical detection box
        rad = tf.localScale.x;
        raylength = rad*Mathf.Cos(Mathf.PI*theta/180)+0.1f;//detection diatance at an angle

    }


    void Update(){
        Player_position =rb.position;
        camerapos = Camera.main.transform.position;//set bottom of the screen
        if (transform.position.y<camerapos.y-11.25f){
            //if player fall through camera
            killplayer();//kill player
        }

        if (haveberry == true && onground == true){
            //consider if the player is safe
            haveberry  = false;
            berrycount= berrycount+1;
            //berry +1
            Debug.Log("berry+1");
        }

        if (alive  == false){
            gameObject.GetComponent<Renderer>().enabled  = alive;
        }
        //printstate();
        
    }   
    

    void FixedUpdate(){
        onwall = false;
        onground = false;
        Collider2D[] colx = Physics2D.OverlapBoxAll(rb.position,xboxSize,0,enviroment); // wall detection
        foreach (Collider2D walx in colx){//detect each point on both side
            colItem =  walx.gameObject;
            if(Player_position.x > colItem.GetComponent<Rigidbody2D>().position.x ){
                //determin player facing
                wallfacing =-1;
            }
            else{
                wallfacing = 1;
            }
            if (colItem.tag =="Wall"){
                onwall = true;
            }
        }
        Collider2D[] coly = Physics2D.OverlapBoxAll(rb.position,yboxSize,0,enviroment); //ground detection
        foreach (Collider2D waly in coly){  //detec each point of onn the bottom
            colItemy =  waly.gameObject;
            if (colItemy.tag =="Wall"){//if contact item is ground
                onground = true;
            }
        }

        poz = rb.position;//record position

        if (Input.GetKey(KeyCode.D)) {//set input direction horizontal
                X_direction = 1;
        }
        else if (Input.GetKey(KeyCode.A)) {
                X_direction = -1;
        }
        else{
            X_direction =0;
        }
        if (Input.GetKey(KeyCode.W)) {//set input direction vertical
                Y_direction = 1;
        }
        else if (Input.GetKey(KeyCode.S)) {
                Y_direction = -1;
        }
        else{
            Y_direction = 0;
        }
        if (onground == true){
            stamina = stamint;//reset stamina
            candash = true;//reset dash
            position_jump = Player_position;
            rb.AddForce(transform.right * speed*Time.deltaTime*X_direction);//move
            if (Input.GetKey(KeyCode.K)){//jump
                    tempx = rb.velocity.x;//perseve x momentum
                    rb.velocity = new Vector2(tempx,0f);//change y speed to 0
                    rb.AddForce(transform.up*jump_speed*Time.deltaTime, ForceMode2D.Impulse);//jump ups
                    jumping = true;//set the playe rto be jumping
            }      
        }
        if (onwall ==true){
            rb.AddForce(transform.right * speed*Time.deltaTime*X_direction*0.5f);//move
            rb.AddForce(transform.up * speed* Time.deltaTime * Y_direction *0.5f);
            if (Input.GetKey(KeyCode.K)){//jump
                    tempx = rb.velocity.x;//perseve x momentum
                    rb.velocity = new Vector2(tempx,0f);//change y speed to 0
                    rb.AddForce(transform.up*jump_speed*Time.deltaTime, ForceMode2D.Impulse);//jump ups
                    jumping = true;//set the playe rto be jumping
            } 
        }
        else{
            rb.AddForce(transform.right * speed*Time.deltaTime*X_direction);//move
        }
        


        if (Input.GetKeyUp(KeyCode.K)){
            tempy= rb.velocity.y;
            if (tempy>=0){
                if (jumping == true){
                   clearvelocityY();//remove y component velocity
                    jumping = false;
                }
            }
        }


        if (Input.GetKey(KeyCode.L) && Time.time > nextdash && candash == true){  //dash
            nextdash = Time.time + dash_cd;//set next dash time
            rb.gravityScale =0;//no down fall from dash
            rb.AddForce(transform.right* X_direction*dash_force*0.3f);//dash
            rb.AddForce(transform.up*Y_direction*dash_force);
            rb.gravityScale = gravity;//set graviy to normal
            candash = false;//wait for cooldown
        }
        //detect where is the player leaving from the wall
        RaycastHit2D hitup = Physics2D.Raycast(Player_position, ang(theta,wallfacing).normalized,raylength,enviroment);
        if (hitup.collider != null){
           
            playertop = true;
        }
        else{
            playertop = false;
        }
        
        RaycastHit2D hitdown = Physics2D.Raycast(Player_position, ang(180-theta,wallfacing).normalized, raylength,enviroment);
        Vector3 v3 = rb.position, temp3 = ang(180 - theta,wallfacing);
        if (hitdown.collider != null){
            //Debug.Log(hitdown.collider.gameObject.name);
            playerbottom = true;
        }
        else{
            playerbottom = false;
        }



        if (Input .GetKey(KeyCode.LeftShift)){
            if(onwall == true && stamina >=0){
                rb.gravityScale = 0; //lower falling speed#
                stamina = stamina - Time.deltaTime;
                //Debug.Log(stamina);
                if (wallgrab == false){ //if not grabing 
                    clearvelocityY();   // clear y momentum
                    wallgrab = true;
                } 
            } 
            else{
                //if player has no stamina or not on a wall
                rb.gravityScale = gravity;
                if (wallgrab == true){//upon leaving a wall
                    wallgrab =false;
                    if (playertop == false && playerbottom == true){
                        rb.AddForce(new Vector2(0,0.5f)*pushforce);
                        //push the player up on to platform
                        StartCoroutine(addfaftert());
                        //push the player to land on platform after delay
                    }
                }
            }
        }
        else{
            rb.gravityScale = gravity;
            wallgrab = false;
        }
        keypos();//set point for item to follow
    }
    
    public Vector2 keypos(){
        //one unit behind the player
        if (X_direction!=0){
            //determin the facing of the player
            position_keyfollow = rb.position + new Vector2 (1f,0)*X_direction*-1;
            lastfacing = X_direction;
        }
        else{
            position_keyfollow = rb.position + new Vector2 (1f,0)*lastfacing*-1;
        }
        return position_keyfollow;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        //for debug
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(poz, xboxSize);
        Gizmos.DrawCube(poz, yboxSize); 
        
    }
    void clearvelocityY(){
        tempx = rb.velocity.x;//record x
        rb.velocity = new Vector2(tempx,0f);//reset y while perserve x
    }
    public void clearvelocityX(){
        tempy = rb.velocity.y;//record y
        rb.velocity = new Vector2(0f,tempy);//reset x while perserve y
    }

    Vector2 diec;
    Vector2 ang(float angle,int fac){
        angle = Mathf.PI*angle / 180;//convert radian to degree
        diec = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        diec.x = fac * diec.x;//change by unit scale
        diec.Normalize();
        return diec;
    }
    IEnumerator addfaftert(){
        yield return 0;
        rb.AddForce(new Vector2(wallfacing,0)*pushforce); //push player on platform
    }   

    public void ResetStamina(){
        stamina = stamint;
        candash = true;
    }
    public void GetKey(bool k){
        //change between have and dont have key
        if (k ==true){
            havekey = true;
        }
        else{
            havekey = false;
        }
    }
    public void getberry(){
        haveberry  = true;//set player to have berry
    }
    
    void printstate(){
        //debug fuction to know player state
        if (onground  == true){
            Debug.Log("onground");
        }
        else if(onwall == true){
            Debug.Log("onwall");
        }
        else{
            Debug.Log("inair");
        }
    }
    public void  killplayer(){
        if(alive == true){
            alive = false;
        }
    }
    
}


