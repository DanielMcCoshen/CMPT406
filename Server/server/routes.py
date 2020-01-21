from server import app
from flask_api import status
from flask import render_template
from server.forms import position_form

location = "centre"

@app.route('/')
@app.route('/index')
def index():
    form = position_form()
    return render_template('index.html', title="choose direction", form=form), status.HTTP_200_OK

@app.route("/game/location/", methods=['POST'])
def set_loc():
    global location
    form = position_form()
    if form.validate_on_submit():
        location = form.option.data
    return status.HTTP_200_OK
    
@app.route("/game/location", methods=['GET'])
def get_loc():
    return location, status.HTTP_200_OK