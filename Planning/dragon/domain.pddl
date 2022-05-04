(define (domain dragon)
    (:requirements :strips :equality :typing)
    (:types location item character loot)
    (:predicates
        (at ?l - location ?c - character)
        (safe ?l - location)
        (has ?a - item ?b - character)
        (contains ?a ?i - item)
        (loot ?i ?k - item)
        (container ?i - item)
        (path ?a ?b - location)
        (strong ?x)
        (weak ?x)
        (dead ?c - character)
        (alive ?c - character)
        (open ?i - item))

    (:action go
        :parameters (?a ?b - location ?c - character)
        :precondition (and
            (at ?a ?c)
            (path ?a ?b)
            (alive ?c)
            (safe ?a))
        :effect (and
            (not (at ?a  ?c))
            (at ?b ?c)))
            
    (:action get
        :parameters (?i - item ?l - location ?c - character)
        :precondition (and
            (at ?l ?c)
            (has ?i ?l))
        :effect (and
            (has ?i ?c)
            (not (has ?i ?l))))

    (:action loot
        :parameters (?x ?i ?k - item ?l - location ?c - character)
        :precondition (and
            (at ?l ?c)
            (has ?i ?l)
            (has ?k ?c)
            (contains ?x ?i)
            (loot ?x)
            (container ?i ?k)
            (alive ?c))
        :effect (and
            (has ?x ?c)
            (not (contains ?x ?i))))

    (:action kill-weak
        :parameters (?c ?t - character ?i - item ?l - location)
        :precondition (and
            (at ?l ?c)
            (at ?l ?t)
            (has ?i ?c)
            (alive ?t)
            (alive ?c)
            (weak ?t)
            (weak ?i))
        :effect (and
            (dead ?t)
            (not (alive ?t))
            (safe ?l)))
            
    (:action kill-strong
        :parameters (?c ?t - character ?i - item ?l - location)
        :precondition (and
            (at ?l ?c)
            (at ?l ?t)
            (has ?i ?c)
            (alive ?t)
            (alive ?c)
            (strong ?t)
            (strong ?i))
        :effect (and
            (dead ?t)
            (not (alive ?t))
            (safe ?l)))
)