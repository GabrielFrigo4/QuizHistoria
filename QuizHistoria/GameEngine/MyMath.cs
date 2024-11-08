﻿using System;

namespace QuizHistoria
{
    static class MyMath
    {
        public static bool pointPoint(float x1, float y1, float x2, float y2)
        {
            if (x1 == x2 && y1 == y2)
            {
                return true;
            }
            return false;
        }

        public static bool pointCircle(float px, float py, float cx, float cy, float r)
        {

            // get distance between the point and circle's center
            // using the Pythagorean Theorem
            float distX = px - cx;
            float distY = py - cy;
            float distance = (float)Math.Sqrt((distX * distX) + (distY * distY));

            // if the distance is less than the circle's
            // radius the point is inside!
            if (distance <= r)
            {
                return true;
            }
            return false;
        }

        public static bool circleCircle(float c1x, float c1y, float c1r, float c2x, float c2y, float c2r)
        {

            // get distance between the circle's centers
            // use the Pythagorean Theorem to compute the distance
            float distX = c1x - c2x;
            float distY = c1y - c2y;
            float distance = (float)Math.Sqrt((distX * distX) + (distY * distY));

            // if the distance is less than the sum of the circle's
            // radii, the circles are touching!
            if (distance <= c1r + c2r)
            {
                return true;
            }
            return false;
        }

        public static bool pointRect(float px, float py, float rx, float ry, float rw, float rh)
        {

            // is the point inside the rectangle's bounds?
            if (px >= rx &&        // right of the left edge AND
                px <= rx + rw &&   // left of the right edge AND
                py >= ry &&        // below the top AND
                py <= ry + rh)
            {   // above the bottom
                return true;
            }
            return false;
        }

        public static bool rectRect(float r1x, float r1y, float r1w, float r1h, float r2x, float r2y, float r2w, float r2h)
        {

            // are the sides of one rectangle touching the other?

            if (r1x + r1w >= r2x &&    // r1 right edge past r2 left
                r1x <= r2x + r2w &&    // r1 left edge past r2 right
                r1y + r1h >= r2y &&    // r1 top edge past r2 bottom
                r1y <= r2y + r2h)
            {    // r1 bottom edge past r2 top
                return true;
            }
            return false;
        }

        public static bool circleRect(float cx, float cy, float radius, float rx, float ry, float rw, float rh)
        {

            // temporary variables to set edges for testing
            float testX = cx;
            float testY = cy;

            // which edge is closest?
            if (cx < rx) testX = rx;      // test left edge
            else if (cx > rx + rw) testX = rx + rw;   // right edge
            if (cy < ry) testY = ry;      // top edge
            else if (cy > ry + rh) testY = ry + rh;   // bottom edge

            // get distance from closest edges
            float distX = cx - testX;
            float distY = cy - testY;
            float distance = (float)Math.Sqrt((distX * distX) + (distY * distY));

            // if the distance is less than the radius, collision!
            if (distance <= radius)
            {
                return true;
            }
            return false;
        }
    }
}
