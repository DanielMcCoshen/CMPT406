from server import app
from flask_api import status
from flask import render_template
from server.forms import position_form

location = "centre"

@app.route('/', methods=['GET', 'POST'])
@app.route('/index')
@app.route("/game/location/", methods=['POST']) # this is bad but I want it working, should be own route
def index():
    global location
    form = position_form()
    if form.validate_on_submit():
        location = form.option.data    
    print(location)
    return render_template('index.html', location=location, title="choose direction", form=form), status.HTTP_200_OK


#def set_loc():
#    global location
#    form = position_form()
   
    
@app.route("/game/location", methods=['GET'])
def get_loc():
    return location, status.HTTP_200_OK