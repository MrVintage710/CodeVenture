using System;
using UnityEngine;
using Jint;

public class CharacterMovment : MonoBehaviour {

    private static Engine engine;
    public CharacterController2D Controller;
    public SeeEvent seeEvent;
    public StartEvent startEvent;

    public String onSee;
    public String onStart;
    
    [Range(-1,1)]
    public float speed = 0.0f;
    public int direction = 1;

    public CapsuleCollider2D seeCollider;

    private void Awake() {
        if (engine == null) {

            engine = new Engine();
            initEngine(engine);
        }

        if (seeEvent == null) {
            seeEvent = new SeeEvent();
            seeEvent.AddListener(seeable => { engine.Execute(onSee); });
        }


        if (startEvent == null) {
            startEvent = new StartEvent();
            startEvent.AddListener(() => { engine.Execute(onStart); });
        }
    }

    private void Start() {
        startEvent.Invoke();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("I see this");
    }

    // Update is called once per frame
    void Update() {
        Controller.Move(speed * direction, false, false);
    }

    private void initEngine(Engine engine) {
        engine.SetValue("log", new Action<object>(Debug.Log));
        engine.SetValue("walk", new Action(this.walk));
        engine.SetValue("run", new Action(this.run));
        engine.SetValue("goLeft", new Action(this.goLeft));
        engine.SetValue("goRight", new Action(this.goRight));
        engine.SetValue("stop", new Action(this.stop));
    }

    private void walk() {
        speed = 0.4f;
    }

    private void run() {
        speed = 0.8f;
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
}
