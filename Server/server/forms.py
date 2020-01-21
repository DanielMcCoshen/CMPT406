from flask_wtf import FlaskForm
from wtforms import StringField, PasswordField, BooleanField, SubmitField, RadioField
from wtforms.validators import DataRequired

class position_form(FlaskForm):
    option = RadioField('Direction', choices=[('centre', 'centre'), ('left', 'left'), ('right', 'right'), ('bottom', 'bottom'), ('top', 'top')])
    submit = SubmitField('submit')