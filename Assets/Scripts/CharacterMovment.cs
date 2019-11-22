using System;
using UnityEngine;
using Jint;
using UnityEngine.Events;

public class CharacterMovment : MonoBehaviour {

    private static Engine engine;
    public CharacterController2D Controller;
    public GameEvent seeEvent;
    public StartEvent startEvent;
    public GameEvent onEvent;

    [TextArea] public String onSee;
    [TextArea] public String onNear;
    [TextArea] public String onStart;
    
    [Range(0,10)]
    public float speed = 1.0f;
    public int direction = 1;
    public bool shouldJump = false;

    public CapsuleCollider2D seeCollider;

    private void Awake() {
        if (engine == null) {
            engine = new Engine();
            initEngine(engine);
        }

        if (seeEvent == null) {
            seeEvent = new GameEvent();
            seeEvent.AddListener(obstacle => {
                engine.SetValue("obstacle", obstacle)
                      .Execute(onSee);
            });
        }
         
        if (onEvent == null) {
            onEvent = new GameEvent();
            onEvent.AddListener(obstacle => {
                engine.SetValue("obstacle", obstacle)
                      .Execute(onNear);
            });
        }


        if (startEvent == null) {
            startEvent = new StartEvent();
            startEvent.AddListener(() => { engine.Execute(onStart); });
        }
    }

    private void Start() {
        startEvent.Invoke();
    }

    public void invokeSeeEvent(IObstacle obstacle) {
        seeEvent.Invoke(obstacle);
    }
    
    public void invokeOnEvent(IObstacle obstacle) {
        onEvent.Invoke(obstacle);
    }

    // Update is called once per frame
    void Update() {
        Controller.Move(speed * direction, false, shouldJump);

        if (shouldJump) shouldJump = false;
    }

    private void initEngine(Engine engine) {
        engine.SetValue("log", new Action<object>(Debug.Log));
        engine.SetValue("walk", new Action(this.walk));
        engine.SetValue("run", new Action(this.run));
        engine.SetValue("goLeft", new Action(this.goLeft));
        engine.SetValue("goRight", new Action(this.goRight));
        engine.SetValue("stop", new Action(this.stop));
        engine.SetValue("jump", new Action(this.jump));
    }

    private void walk() {
        speed = 1f;
    }

    private void run() {
        speed = 2f;
    }

    private void stop() {
        speed = 0.0f;
    }

    private void goLeft() {
        direction = -1;
    }

    private void goRight() {
        direction = 1;
    }

    private void jump() {
        shouldJump = true;
    }
}
