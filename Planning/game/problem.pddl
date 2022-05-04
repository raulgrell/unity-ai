(define (problem game)
    (:domain game)

    (:objects 
        store street house - location
        john sam - character
        gun - item)

    (:init
        (at john house)
        (at sam street)
        (at gun store)
        (is-weapon gun)
        (path street store)
        (path store street)
        (path street house)
        (path house street)
        (alive john)
        (alive sam))

    (:goal
        (and 
            (has john gun)
            (dead sam)
        )))
