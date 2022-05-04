(define (problem name)
    (:domain robot)

    (:objects 
        r1 r2 - room
        b1 b2 b3 b4 - box
        left right - arm)

    (:init
        (robot-at r1)
        (box-at b1 r1)
        (box-at b2 r1)
        (box-at b3 r1)
        (box-at b4 r1)
        (free left)
        (free right))

    (:goal
        (and
            (box-at b1 r2)
            (box-at b2 r2)
            (box-at b3 r2)
            (box-at b4 r2))))
