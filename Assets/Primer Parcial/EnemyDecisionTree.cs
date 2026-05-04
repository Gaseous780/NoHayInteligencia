//using JetBrains.Annotations;
//using System;
//using UnityEngine;
//using UnityEngine.Rendering;

//public class EnemyDecisionTree : MonoBehaviour
//{
//    private DecisionNode rootNode;
//    private DecisionNode questionAttackNode;

//    private void Awake()
//    {
//        ActionNode patrolNode = new ActionNode(enemy => enemy.Patrol());//llamar funcion sin un nombre,arrow function
//        //ActionNode pursuitNode = new ActionNode(enemy => enemy.Pursuit());
//        ActionNode attackNode = new ActionNode(enemy => enemy.Attack());

//        //WeightedRandomActionNode seePlayerNode = new WeightedRandomActionNode (float, System.Action<EnemyController>[]
//        //{
//        //    (70f,enemy=>enemy.Pursuit()),
//        //    (20f, enemy => enemy.Patrol());
//        //}
//        //questionAttackNode = new QuestionNode(context => context.los.IsRangeAttack(context.self, context.player),
//        //attackNode, pursuitNode);

//        rootNode = new QuestionNode(context => context.los.IsRange(context.self, context.player)
//        && context.los.IsAngle(context.self, context.player) &&
//        context.los.IsObstacle(context.self, context.player),
//        questionAttackNode, patrolNode);

//    }

//    public void Evaluate(EnemyController enemy, EnemyContext context)
//    {
//        rootNode.Evaluate(enemy, context);
//    }

//}

//public class ActionNode : DecisionNode
//{
//    private (float weight, Action<EnemyController> action)[] options;

//    public ActionNode ((float weight,Action<EnemyController> action)[]options)
//    {
//        this.options= options;
//    }

//    public override void Evaluate(EnemyController enemy, EnemyContext context)
//    {
//        float totalweight = 0;
//        foreach (var option in options)
//        {
//            totalweight += option.weight;
//        }

//        float randomValue=UnityEngine.Random.Range(0, totalweight);
//        float currentWeight = 0;
//        foreach (var option in options)
//        {
//            currentWeight += option.weight;
//            if (randomValue <= currentWeight)
//            {
//                option.action(enemy);
//                return;
//            }
//        }
//    }
//}
