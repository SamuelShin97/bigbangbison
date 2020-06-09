// This behavior will only be called when a bison is in the square box in front of the player(s)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Herd/Behavior/PlayerFacingDirectionAlignment")]
public class PlayerFacingDirectionAlignmentBehavior : FilteredHerdBehavior
{
    public override Vector3 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd)
    {
        // If there are no bison or players nearby, we chillin
        if (context.Count == 0) return Vector3.zero;

        // This is what will be returned in the end
        Vector3 direction = Vector3.zero;

        //This should return the player(s), if there are any
        List<Transform> filterContext = (filter == null) ? context : filter.Filter(agent, context);

        // For each player
        foreach (Transform item in filterContext)
        {
            // Determine if player is the right herd
            if (item.GetComponent<HerdShepherd>().myHerd != agent.AgentHerd) continue;

            //Use this to see distance between bison and player(s), and increase speed depending on this
            float distance = Vector3.Distance(item.transform.position, agent.transform.position);

            //Assuming the max distance in front of a player that this behavior triggers is 6
            //multiplier output ranges from 1x to 7x
            float multiplier = 7f - distance;

            //This line makes the output instead range from 1/6 to 7/6,
            //where 1/6 is multiplier if 6 units away, and 7/6 is multiplier if 0 units away
            // (So you move slightly faster than player if right on top of them, and slower than them if further away)
            multiplier /= 6f;

            Vector3 playerDirection = item.transform.forward;
            Vector3 bisonDirection = agent.transform.forward;
            float directionOutcome = Vector3.Dot(playerDirection, bisonDirection);

            //If bison is pointing in the opposite direction, double multiplier
            if (directionOutcome == -1)
                multiplier *= 2;

            //If bison is poining in perpendicular direction, 1.5x multiplier
            else if (directionOutcome == 0)
                multiplier *= 1.5f;

            direction = playerDirection;
            //direction *= multiplier;        // Comment this out if you don't want speed to change
        }

        direction *= 6; // Make sure it's visible
        return direction;

    }

    public override Vector3 CalculateMove(OnlineHerdAgent agent, List<Transform> context, OnlineHerd herd)
    {
        // If there are no bison or players nearby, we chillin
        if (context.Count == 0) return Vector3.zero;

        // This is what will be returned in the end
        Vector3 direction = Vector3.zero;

        //This should return the player(s), if there are any
        List<Transform> filterContext = (filter == null) ? context : filter.Filter(agent, context);

        // For each player
        foreach (Transform item in filterContext)
        {
            //Use this to see distance between bison and player(s), and increase speed depending on this
            float distance = Vector3.Distance(item.transform.position, agent.transform.position);

            //Assuming the max distance in front of a player that this behavior triggers is 6
            //multiplier output ranges from 1x to 7x
            float multiplier = 7f - distance;

            //This line makes the output instead range from 1/6 to 7/6,
            //where 1/6 is multiplier if 6 units away, and 7/6 is multiplier if 0 units away
            // (So you move slightly faster than player if right on top of them, and slower than them if further away)
            multiplier /= 6f;

            Vector3 playerDirection = item.transform.forward;
            Vector3 bisonDirection = agent.transform.forward;
            float directionOutcome = Vector3.Dot(playerDirection, bisonDirection);

            //If bison is pointing in the opposite direction, double multiplier
            if (directionOutcome == -1)
                multiplier *= 2;

            //If bison is poining in perpendicular direction, 1.5x multiplier
            else if (directionOutcome == 0)
                multiplier *= 1.5f;

            direction = playerDirection;
            //direction *= multiplier;        // Comment this out if you don't want speed to change
        }

        direction *= 6; // Make sure it's visible
        return direction;

    }
}