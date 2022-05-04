(define (problem dragon)
    (:domain dragon)

    (:objects 
        store castle town forest river cave - location
        john dragon troll - character
        sword key bow chest - item)

    (:init
        ; Characters
        (at castle dragon)
        (at town john)
        (at forest troll)

        (alive john)
        (alive troll)
        (alive dragon)

        ; Locations
        (path castle town)
        (path town castle)

        (path store town)
        (path town store)
        
        (path town forest)
        (path forest town)
        
        (path forest river)
        (path river forest)
        
        (path river cave)
        (path cave river)

        ; Items
        (has chest cave)
        (has sword store)
        (has key river)

        (container chest key)
        (loot bow)
        (contains bow chest)

        (strong bow)
        (strong dragon)
        (weak sword)
        (weak troll)
        
        ;Safe
        (safe store)
        (safe town)
        (safe river)
        (safe cave))

    (:goal
        (and 
            (dead dragon)
        )))
