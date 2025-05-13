class ForewarnedModule
{
	#socket;

	constructor() {
		this.#socket = new WebSocket('ws://' + location.host + '/overlay/plugin/forewarned');
		this.#socket.onmessage = this.#OnMessage.bind(this);
	}

	#OnMejais(data) {
		for (var i = 0; i < data.length; i++) {
			var mejai = data[i];
			//TODO
		}
	}

	#UpdateEvidence(data) {
		if (data.hasOwnProperty('id') && data.hasOwnProperty('is_found') && data.hasOwnProperty('is_ruled_out')) {
			var isFound = data['is_found'];
			var isRuledOut = data['is_ruled_out'];
			var evidenceDiv = document.getElementById(data['id']);
			var evidenceElement = evidenceDiv.getElementsByClassName("evidence_img_div").item(0);
			var ruleOutElement = evidenceDiv.getElementsByClassName("rule_out_img_div").item(0);
			if (isFound || isRuledOut)
				evidenceElement.style.display = 'inline-block';
			else
				evidenceElement.style.display = 'none';

			if (isRuledOut)
				ruleOutElement.style.display = 'inline-block';
			else
				ruleOutElement.style.display = 'none';
		}
	}

	#CreateEvidence(data) {
		if (data.hasOwnProperty('id') &&
			data.hasOwnProperty('path') &&
			data.hasOwnProperty('is_found') &&
			data.hasOwnProperty('is_ruled_out')) {
			var id = data['id'];
			var isFound = data['is_found'];
			var isRuledOut = data['is_ruled_out'];

			var evidenceDiv = document.createElement('div');
			evidenceDiv.id = id;
			evidenceDiv.className = 'evidence_div';

			var evidenceImgDiv = document.createElement('div');
			evidenceImgDiv.className = 'evidence_img_div';
			var evidenceImg = document.createElement('img');
			evidenceImg.className = 'evidence_img';
			evidenceImg.src = data['path'];
			evidenceImgDiv.appendChild(evidenceImg);
			if (isFound || isRuledOut)
				evidenceImgDiv.style.display = 'inline-block';
			else
				evidenceImgDiv.style.display = 'none';

			var ruleOutImgDiv = document.createElement('div');
			ruleOutImgDiv.className = 'rule_out_img_div';
			var ruleOutImg = document.createElement('img');
			ruleOutImg.className = 'rule_out_img';
			ruleOutImg.src = 'forewarned/assets/cross.png';
			ruleOutImgDiv.appendChild(ruleOutImg);
			if (isRuledOut)
				ruleOutImgDiv.style.display = 'inline-block';
			else
				ruleOutImgDiv.style.display = 'none';

            evidenceDiv.appendChild(evidenceImgDiv);
			evidenceDiv.appendChild(ruleOutImgDiv);
			document.getElementById('evidences').appendChild(evidenceDiv);
		}
	}

	#OnMessage(event) {
		var eventJson = JSON.parse(event.data);
		if (eventJson.hasOwnProperty('type')) {
			var type = eventJson['type'];
			switch (type) {
				case 'welcome':
					{
						if (eventJson.hasOwnProperty('evidences')) {
							var evidences = eventJson['evidences'];
							for (var i = 0; i < evidences.length; i++)
								this.#CreateEvidence(evidences[i]);
						}
						if (eventJson.hasOwnProperty('mejais'))
							this.#OnMejais(eventJson['mejais']);
						break;
					}
				case 'reset':
					{
						var slides = document.getElementsByClassName("evidence_div");
						for (var i = 0; i < slides.length; i++) {
							var evidenceDiv = slides.item(i);
							evidenceDiv.getElementsByClassName("evidence_img_div").item(0).style.display = 'none';
							evidenceDiv.getElementsByClassName("rule_out_img_div").item(0).style.display = 'none';
						}
						break;
					}
				case 'evidence':
					{
						if (eventJson.hasOwnProperty('evidence'))
							this.#UpdateEvidence(eventJson['evidence']);
						if (eventJson.hasOwnProperty('mejais'))
							this.#OnMejais(eventJson['mejais']);
						break;
					}
			}
		}
	}
}

var streamGlassModuleClient;

function OnLoad()
{
	streamGlassModuleClient = new ForewarnedModule();
}