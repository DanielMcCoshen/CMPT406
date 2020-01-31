import unittest
from game import game_room, rooms

class test_singleton(unittest.TestCase):
    def is_same(self):
        list1 = rooms.get_map()
        list2 = rooms.get_map()

        self.assertEqual(id(list1), id(list2))

class test_game_room(unittest.TestCase):
    def test_adds_job(self):
        room = game_room()
        room.add_job(55)
        room.add_job(66)
        self.assertEqual(room.get_current_job(), 55)
        self.assertEqual(room.next_job(), 66)
        self.assertEqual(room.get_current_job(), 66)

if __name__ == '__main__':
    unittest.main()