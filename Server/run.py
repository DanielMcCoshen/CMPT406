from server import app
from model import rooms, roomlist, itemlist

print("begining setup")
rooms.get_map()
roomlist.get()
itemlist.item()

