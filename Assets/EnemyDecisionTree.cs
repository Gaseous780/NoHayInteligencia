using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyDecisionTree : MonoBehaviour
{
    private DecisionNode rootNode;

    private void Awake()
    {
        ActionNode patrolNode = new ActionNode(enemy => enemy.Patrol());//llamar funcion sin un nombre,arrow function
        ActionNode pursuitNode = new ActionNode(enemy => enemy.Pursuit());

        rootNode = new QuestionNode(context => context.los.IsRange(context.self, context.player)
        && context.los.IsAngle(context.self, context.player) &&
        context.los.IsObstacle(context.self, context.player),
        pursuitNode, patrolNode);
    }

        public void Evaluate(EnemyController enemy, EnemyContext context)
        {
            rootNode.Evaluate(enemy, context);
        }
}
