using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
   public Task root;
   public World world;
   
   private TaskStatus status = TaskStatus.None;
   private Task current;
   private NavMeshAgent agent;
   public int ammo;

   public NavMeshAgent Agent => agent;

   private void Start()
   {
      agent = GetComponent<NavMeshAgent>();
      
      root = new Sequence();
      
      var exercise1Root = new Selector();
      exercise1Root.SetParent(root);
      
      var moveIntoOpenRoom = new Sequence();
      moveIntoOpenRoom.AddChild(new CheckDoorIsOpen("Door"));
      moveIntoOpenRoom.AddChild(new MoveTask("Room"));
      moveIntoOpenRoom.SetParent(exercise1Root);
      
      var moveIntoClosedRoom = new Sequence();
      moveIntoClosedRoom.AddChild(new MoveTask("Door"));
      moveIntoClosedRoom.SetParent(exercise1Root);
      
      var openRoom = new Selector();
      openRoom.SetParent(moveIntoClosedRoom);
      
      var openUnlockedRoom = new Sequence();
      openUnlockedRoom.AddChild(new CheckDoorUnlocked("Door"));
      openUnlockedRoom.AddChild(new OpenDoorTask("Door"));
      openUnlockedRoom.SetParent(openRoom);
      
      var openLockedRoom = new Sequence();
      openLockedRoom.AddChild(new BargeDoorTask("Door"));
      openLockedRoom.AddChild(new CheckDoorIsOpen("Door"));
      openLockedRoom.SetParent(openRoom);
      
      moveIntoClosedRoom.AddChild(new MoveTask("Room"));
      
      var exercise2Root = new RepeatDecorator();
      exercise2Root.SetParent(root);

      var seq = new Sequence();
      seq.SetParent(exercise2Root);
      
      var actions = new Parallel();
      actions.AddChild(new AttackTask("Enemy"));
      actions.SetParent(seq);
      
      var reload = new Selector();
      reload.SetParent(actions);
      
      var checkReload = new Sequence();
      checkReload.AddChild(new CheckAmmo(1));
      checkReload.AddChild(new ReloadTask());
      checkReload.SetParent(reload);
      
      reload.AddChild(new Success());
      
      current = root;
   }

   void Update()
   {
      if (status == TaskStatus.None || status == TaskStatus.Running)
         status = current.Run(this, world);
   }    
   
   public bool isAtDestination()
   {
      return !agent.pathPending
             && agent.remainingDistance <= agent.stoppingDistance;
   }

   public void Reload()
   {
      Debug.Log("=== RELOADING ===");
      ammo = 10;
   }

   public void Attack(string target)
   {
      Debug.Log("Attacked, ammo remaining: " + ammo);
   }
}