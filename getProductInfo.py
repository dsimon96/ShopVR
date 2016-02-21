# Get Product Info

import requests, urllib, string
from HTMLParser import HTMLParser


def getJSON(id_type, product_id):
    api = 'products/v3' # products or location
    r = requests.get('https://api.target.com/' + api + '/?key=J5PsS2XGuqCnkdQq0Let6RSfvU7oyPwF' 
                     + '&id_type=' + id_type + '&product_id=' + product_id,
                     auth=('user', 'pass'))
    return r.json() # <type 'dict'>


# Returns a string of the Product Name or the bool False
def getName(json):
    name = False # did not find a name yet
    if 'product_composite_response' in json:
        tempName = json['product_composite_response']
        if 'items' in tempName:
            tempName = tempName['items']
            if len(tempName) > 0:
                tempName = tempName[0]
                if 'general_description' in tempName:
                    name = str(tempName['general_description'])
    return name


# Returns string of URL of Product's page or False
def getHTML(json):
    pageLink, HTML = False, False
    if 'product_composite_response' in json:
        tempPageLink = json['product_composite_response']
        if 'items' in tempPageLink:
            tempPageLink = tempPageLink['items']
            if len(tempPageLink) > 0:
                tempPageLink = tempPageLink[0]
                if 'data_page_link' in tempPageLink:
                    pageLink = str(tempPageLink['data_page_link'])

    if pageLink != False:
        r = requests.get(pageLink, auth=('user', 'pass'))
        HTML = r.content

    return HTML


# Returns string of img URL or the bool False
def getImgURL(HTML):
    imgURL = False
    imgTag = 'name="twitter:image:src" content="'
    startIndex = HTML.find(imgTag) + len(imgTag)
    endIndex = HTML.find('"', startIndex) # end of quote ends url

    if (startIndex < endIndex and endIndex < len(HTML)):
        imgURL = HTML[startIndex:endIndex]

    return imgURL


def makeReadable(desc):
    desc = desc.replace('&rdquo;', '"')
    desc = desc.replace('&rsquo;', "'")
    desc = desc.replace('&#39;', "'")
    desc = desc.replace('&ndash;', '-')
    desc = desc.replace('<br>', '\n')
    desc = desc.replace('&#60;br', '\n')
    desc = desc.replace('&trade;', '(TM)')
    desc = desc.replace('&nbsp;', ' ') # non breaking space
    desc = desc.replace('&bull;', '\n*')

    desc = desc.replace('&#60;', '')
    desc = desc.replace('p&#62;', '')
    desc = desc.replace('&#62;', '')
    desc = desc.replace('&reg;', '')
    desc = desc.replace('&#47;', '')

    return desc


# Returns string of description or the bool False
def getDescription(HTML):
    desc = False
    parser = HTMLParser()
    descTag = 'property="og:description" content="'
    startIndex = HTML.find(descTag) + len(descTag)
    endIndex = HTML.find('"', startIndex) # end of quote ends desc

    if (startIndex < endIndex and endIndex < len(HTML)):
        desc = HTML[startIndex:endIndex]
        
        # Get rid of most HTML characters
        parser = HTMLParser()
        desc = str(parser.unescape(desc))

        # Send through handmade parser
        desc = makeReadable(desc)

    return desc


# Returns list of price in dollars and cent (two ints) or the bool False
def getPrice(HTML):
    price, priceList = False, False
    priceTag = 'class="offerPrice" itemprop="price">$'
    startIndex = HTML.find(priceTag) + len(priceTag)
    endIndex = HTML.find('<', startIndex) # end of span is character that signals end

    if (startIndex < endIndex and endIndex < len(HTML)):
        price = HTML[startIndex:endIndex]

    # Get dollar and cents separately
    if price != False:
        priceList = price.split('.')
        for i in xrange(len(priceList)):
            priceList[i] = int(priceList[i])

    return priceList


# Info to be sent to Unity
def productInfo(id_type, product_id):
    json = getJSON(id_type, product_id) # Specify product
    HTML = getHTML(json) # string of HTML of product's webpage

    name = getName(json) # site's name for product
    imgURL = getImgURL(HTML)
    desc = getDescription(HTML) # string
    priceList = getPrice(HTML)

    if (name != False and desc != False and priceList != False and imgURL != False):
        assert(len(priceList) > 1)
        priceDollars = priceList[0]
        priceCents = priceList[1]

        info = dict()
        info['productName'] = name
        info['priceDollars'] = priceDollars
        info['priceCents'] = priceCents
        info['description'] = desc

        # Create txt file with info of product for Unity
        f = open(name + '.txt', 'w')
        f.write(str(info))

        # Create PNG file from string of URL
        productImgFile(imgURL, name)
        

def productImgFile(imgURL, name):
    filename = name + '.png'
    urllib.urlretrieve(imgURL, filename)

