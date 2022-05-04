(define (domain robot)
    (:requirements :strips :equality :typing)
    (:types room box arm)

    ;(:constants)

    (:predicates
        (robot-at ?r - room)
        (box-at ?b - box ?r - room)
        (free ?a - arm)
        (holding ?a - arm ?b - box))

    (:action move
        :parameters (?f ?t - room)
        :precondition (and (robot-at ?f))
        :effect (and
            (robot-at ?t)
            (not (robot-at ?f))))

    (:action load
        :parameters (?a - arm ?b - box ?r - room)
        :precondition (and (free ?a) (box-at ?b ?r) (robot-at ?r))
        :effect (and
            (holding ?a ?b)
            (not (free ?a))
            (not (box-at ?b ?r))))

    (:action unload
        :parameters (?a - arm ?b - box ?r - room)
        :precondition (and (holding ?a ?b) (robot-at ?r))
        :effect (and
            (not (holding ?a ?b))
            (free ?a)
            (box-at ?b ?r))))